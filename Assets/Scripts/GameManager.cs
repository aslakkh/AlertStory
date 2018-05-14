using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
    investigator,
    eventhandler,
    paused
}

//Defines eventargs to be sent in the stateChanged event
public class GameStateEventArgs : EventArgs
{
    public GameState newState { get; set; }
}

public class GameManager : MonoBehaviour {

    public int score;
    public int privateScore;
    public int dayCount;
    public int turnCount;
    public int endDay; // The day which the game ends on regardless of story progress

    public Character playerCharacterScriptableObject; //reference to player character to be loaded on play
    [HideInInspector]
    public Character playerCharacter; //playerCharacter used during play
    public CharacterList characterListScriptableObject; //reference to character list to be loaded on play
    [HideInInspector]
    public List<Character> characterList; //characterList used during play


    // Dict with day-number as key and a list of information from information package as values
    private Dictionary<int, List<string>> _informationDict;
    private Dictionary<int, List<string>> informationPackageDict;
    private List<string> informationPackageList = new List<string>();
    public List<Objective> objectivesList;
    public Dictionary<int, List<Objective>> objectivesDict;
    public InformationPackageManager informationPackageManager;

    public RequirementDict backupRequirementDict; // Used if requirement dict is empty
    public StringSettingDictionary requirementDict;
    private StoryEventChoiceDictionary _eventsFired;
    private EventManager eventManager;
    private SceneLoader sceneLoader; //reference to sceneloader
    private GameState _gameState;

    //gameState property. Publishes event on change
    public GameState gameState
    {
        get { return _gameState; }
        private set
        {
            if (_gameState != value)
            {
                _gameState = value;
                OnStateChanged(); //publish event stateChanged
            }
        }
    }

    //event to be published after gamestate change. Contains list of method pointers (subscribers)
    public event EventHandler<GameStateEventArgs> stateChanged;

    //Set this when new state is added.
    public StringSettingDictionary requirements {
        get { return requirementDict; }
        set { requirementDict = value; }
    }

    public Dictionary<int, List<Objective>> objectives {
        get { return objectivesDict; }
        set { objectivesDict = value; }
    }
    
    //Add all events that has fired here.
    public StoryEventChoiceDictionary eventsFired {
        get { return _eventsFired; }
        set { _eventsFired = value; }
    }

    //Contains objectivesDict displayed in dropdown
    public Dictionary<int, List<string>> informationDict
    {
        get { return _informationDict; }
        set { _informationDict = value; }
    }

    public List<string> informationPackage
    {
        get { return informationPackageList; }
        set { informationPackageList = value; }
    }

    //Singleton instanciating
    public static GameManager Instance { get; private set; }

    private void Awake() {
        //Check if there are any other instances conflicting
        if ( Instance != null && Instance != this) {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        if (Instance == null)
        {
            Instance = GameObject.FindObjectOfType<GameManager>();
            if (Instance == null)
            {
                Instance = this; //save singleton instance
            }
        }

        
        // Makes sure that we don't destroy between scenes
        DontDestroyOnLoad(gameObject);

        //eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        informationPackageManager = GameObject.Find("InformationPackageManager").GetComponent<InformationPackageManager>();
        objectivesDict = new Dictionary<int, List<Objective>>();

        // for playtesting eventscene
        if (requirementDict == null || requirementDict.Count == 0)
        {
            if (backupRequirementDict != null)
            {
                requirementDict = backupRequirementDict.requirementDictionary;
            }
            else
            {
            }
        }

        // Instantiating objectivesDict
        int i = 1;
        foreach (Objective obj in objectivesList) {
            List<Objective> objectiveList1 = new List<Objective>();
            objectiveList1.Add(obj);
            objectivesDict.Add(i, objectiveList1);
            i++;
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
            informationPackageManager = GameObject.Find("InformationPackageManager").GetComponent<InformationPackageManager>();
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }
    }

    private void Start()
    {
        LoadScriptableObjectCopies();

        gameState = GameState.investigator;
        _eventsFired = new StoryEventChoiceDictionary();
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        //dayCount = 0;
    }

    public bool FireEvent() {
        StoryEvent eventFired = eventManager.InitializeEvent(); //fires first suitable event
        if (eventFired)
        {
            gameState = GameState.eventhandler;
            _eventsFired.Add(eventFired, null);
        }
        else
        {
            if(gameState != GameState.investigator)
            {
                gameState = GameState.investigator;
            }
        }
        return eventFired;

    }

    //handles publishing of stateChanged event
    protected virtual void OnStateChanged()
    {
        if(stateChanged != null) //there are subscribers to the event
        {
            stateChanged(this, new GameStateEventArgs() { newState = gameState}); //publishes event with new gameState added to eventargs
        }
    }

    //handles a choice taken in a story event. Applies any scores, and either fires a new event or returns to investigator state
    public void HandleChoice(StoryEvent storyEvent, Choice choice)
    {
        _eventsFired[storyEvent] = choice;

        //add scores
        if(choice.scores != null)
        {
            foreach (Score score in choice.scores)
            {
                if(!string.IsNullOrEmpty(score.requirementName))
                {
                    //score has a requirement, check if requirement matches requirementdict
                    if (requirementDict.ContainsKey(score.requirementName) && requirementDict[score.requirementName] == score.setting)
                    {
                        AddToScore(score.value);
                    }
                }
                else //score has no requirement
                {
                    AddToScore(score.value);
                }
                

            }
        }
        //Loops for each FunctionCall added.
        foreach (FunctionCall functionCall in choice.functionCalls) {
            functionCall.triggerFunction();
        }
        gameState = GameState.investigator;
        if (storyEvent.fireNextEventImmediately)
        {
            FireEvent();
        }
        else
        {
            if (choice.endGameTrigger)
            {
                sceneLoader.LoadEndScene();
            }
            
        }

    }

    //choice handler for choices made in events with no storyevents attached
    public void HandleChoice()
    {
        gameState = GameState.investigator;
    }

    public void NextDay(bool batteryDepleted)
    {
            StartCoroutine(FireRemainingEvents(batteryDepleted));
    }

    public IEnumerator EndDay(bool batteryDepleted)
    {
        //validate information package
        informationPackageManager.ValidateInformationGathered();
        informationPackage.Clear();

        if (batteryDepleted)
        {
            //fire an event to signal battery depletion
            gameState = GameState.eventhandler;
            eventManager.InstantiateBatteryDepletionEvent();
        }
        else //information package was submitted
        {
            gameState = GameState.eventhandler;
            eventManager.InstantiateInformationPackageSentEvent();
        }
        yield return new WaitUntil(() => gameState == GameState.investigator);


        //end day
        if (++dayCount >= endDay)
        {
            sceneLoader.LoadEndScene();
        }
        else
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            sceneLoader.LoadNextDay();
        }
    }

    public IEnumerator FireRemainingEvents(bool batteryDepleted)
    {
        //fire all remaining events in eventlist
        bool eventFired = FireEvent();
        if (eventFired)
        {
            yield return new WaitUntil(() => gameState == GameState.investigator);
            StartCoroutine(FireRemainingEvents(batteryDepleted));
        }
        else
        {
            StartCoroutine(EndDay(batteryDepleted));
        }
        
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    // Getters
    public int GetDayCount()
    {
        return dayCount;
    }

    // Setters
    public void ResetDayCount()
    {
        this.dayCount = 0;
    }

    //load copies of scriptableobjects to be used during runtime
    public void LoadScriptableObjectCopies()
    {
        //copy playercharacter
        playerCharacter = Instantiate(playerCharacterScriptableObject) as Character;
        playerCharacter.friendsbookProfile = Instantiate(playerCharacterScriptableObject.friendsbookProfile) as FriendsbookProfile;


        characterList = new List<Character>();
        foreach(Character c in characterListScriptableObject.list)
        {
            Character characterClone = Instantiate(c) as Character;
            characterClone.friendsbookProfile = Instantiate(c.friendsbookProfile) as FriendsbookProfile;
            characterList.Add(characterClone);
        }

    }


    //pause or unpaused
    public void SetPaused()
    {
        if(gameState == GameState.paused)
        {
            gameState = GameState.investigator;
        }
        else
        {
            gameState = GameState.paused;
        }

    }
}
