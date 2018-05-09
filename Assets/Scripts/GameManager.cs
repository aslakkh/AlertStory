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
    public Character playerCharacter; //reference to scriptable object holding information about playercharacter
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

        // Instantiate Lists needed to create the objectives
        objectivesDict = new Dictionary<int, List<Objective>>();
        List<Objective> objectiveList1 = new List<Objective>();
        List<Objective> objectiveList2 = new List<Objective>();
        List<Objective> objectiveList3 = new List<Objective>();

        // Make a list of tasks for day 1
        List<string> taskList = new List<string>();
        taskList.Add("Trine");
        taskList.Add("Sci-fi Street 12, 540 Atlantis Space Farm, North Mars Colony");
        taskList.Add("Trine_not_the_thrine_game_u_silly@realTrine.com");
        taskList.Add("Theres no phones on mars... yet");

        // Add tasklist to day 1 objective 1
        objectiveList1.Add(new Objective(
            "Add all the personal information you can find about your friends to the Information Package.", 
            taskList
        ));

        // Add tasklist to day 1 objective 2
        taskList.Clear();
        taskList.Add("blog where");
        objectiveList1.Add(new Objective(
            "Find out what Tom did last week end.", 
            taskList
        ));

        // Reset tasks, and make tasks for day 2
        taskList.Clear();
        taskList.Add("");
        taskList.Add("");

        // Add tasklist to day 2
        objectiveList2.Add(new Objective("Dag 2", taskList));

        // Reset tasks, and make tasks for day 3
        taskList.Clear();
        taskList.Add("");
        taskList.Add("");

        // Add tasklist to day 3
        objectiveList3.Add(new Objective("Dag 3", taskList));

        // Add correct list to correct day
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
        //TODO: implement proper state flow
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
}
