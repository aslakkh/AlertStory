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
    private int dayCount;
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
    public RequirementDict backupRequirementDict; // Used if requirement dict is empty
    public StringSettingDictionary requirementDict;
    private StoryEventChoiceDictionary _eventsFired;
    private EventManager eventManager;
    private SceneLoader sceneLoader; //reference to sceneloader
    private GameState _gameState;
    private Dictionary<int, List<string>> informationPackageDict;
    private List<string> informationPackageList = new List<string>();
    public List<Objective> objectivesList;
    public Dictionary<int, List<Objective>> objectivesDict;
    public InformationPackageManager informationPackageManager;

    //gameState property. Publishes event on change
    private GameState gameState
    {
        get { return _gameState; }
        set
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
        int i = 0;
        List<string> taskList = new List<string>();
        taskList.Add("Ola Nordmann");
        List<Objective> objectiveList1 = new List<Objective>();
        objectiveList1.Add(new Objective("desc", taskList));
        List<Objective> objectiveList2 = new List<Objective>();
        objectiveList2.Add(new Objective("desc", taskList));
        List<Objective> objectiveList3 = new List<Objective>();
        objectiveList3.Add(new Objective("desc", taskList));
        foreach (Objective obj in objectivesList) {
            if (i <= 2) {
                objectiveList1.Add(obj);
            } else if (i <= 4) {
                objectiveList2.Add(obj);
            } else {
                objectiveList3.Add(obj);
            }
            i++;
        }
        objectivesDict.Add(1, objectiveList1);
        objectivesDict.Add(2, objectiveList2);
        objectivesDict.Add(3, objectiveList3);

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
        dayCount = 0;
    }

    public bool FireEvent() {
        Debug.Log(eventManager);
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

    protected virtual void onScoreChanged()
    {

    }

    public void HandleChoice(StoryEvent storyEvent, Choice choice)
    {
        //TODO: Add score handling
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
        
        if (storyEvent.fireNextEventImmediately)
        {
            FireEvent();
        }
        else
        {
            gameState = GameState.investigator;
            if (choice.endGameTrigger)
            {
                sceneLoader.LoadEndScene();
            }
            
        }

    }

    public void NextDay() {

        StartCoroutine(FireRemainingEvents());
          
    }

    public IEnumerator FireRemainingEvents()
    {
        bool eventFired = FireEvent();
        Debug.Log("EventFired: " + eventFired);
        if (eventFired)
        {
            yield return new WaitUntil(() => gameState == GameState.investigator);
            StartCoroutine(FireRemainingEvents());
        }
        else
        {
            informationPackageManager.ValidateInformationGathered();
            informationPackage.Clear();
            if (++dayCount >= endDay)
            {
                sceneLoader.LoadEndScene();
            }
            else
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("daycount:" + dayCount);
                sceneLoader.LoadNextDay();
            }
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
}
