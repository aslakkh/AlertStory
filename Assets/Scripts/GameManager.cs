using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Dictionary<int, Objective> objectives;
    public RequirementDict backupRequirementDict; // Used if requirement dict is empty
    public StringSettingDictionary requirementDict;
    private StoryEventChoiceDictionary _eventsFired;
    private EventManager eventManager;
    private SceneLoader sceneLoader; //reference to sceneloader
    private GameState _gameState;
    private Dictionary<int, List<string>> informationPackageDict;
    private List<string> informationPackageList = new List<string>();

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
    
    //Add all events that has fired here.
    public StoryEventChoiceDictionary eventsFired {
        get { return _eventsFired; }
        set { _eventsFired = value; }
    }

    //Contains objectives displayed in dropdown
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
        // Here we save our singleton instance
        Instance = this;
        
        // Makes sure that we don't destroy between scenes
        DontDestroyOnLoad(gameObject);

        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();

        // for playtesting eventscene
        if (requirementDict == null || requirementDict.Count == 0)
        {
            if (backupRequirementDict != null)
            {
                requirementDict = backupRequirementDict.requirementDictionary;
            }
            else
            {
                Debug.Log("backupRequirementdict in GameManager == Null");
            }
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
        StoryEvent eventFired = eventManager.InitializeEvent(); //fires first suitable event
        if (eventFired)
        {
            gameState = GameState.eventhandler;
            _eventsFired.Add(eventFired, null);
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
            if (choice.endGameTrigger)
            {
                sceneLoader.LoadEndScene();
            }
            gameState = GameState.investigator;
        }

    }

    public void NextDay() {
        
        //Will be called untill there is no more Events in List
        bool eventFired;
        do {
            eventFired = FireEvent();
        } while (eventFired);
        
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

    public void AddToScore(int value)
    {
        score += value;
    }

    //getters
    public int GetDayCount()
    {
        return dayCount;
    }
}
