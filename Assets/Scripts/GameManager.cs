using System;
using System.Linq;
using System.Collections.Generic;
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
    public int dayCount {
        get {
            return this.dayCount;
        }
        set {
            dayCount += value;
            if (dayCount >= endDay)
            {
                SceneManager.LoadScene("TEMP_EndGameScene");
            }
        }
    }
    public int turnCount;
    public int endDay; // The day which the game ends on regardless of story progress
    public Character playerCharacter; //reference to scriptable object holding information about playercharacter
    private List<string> _objectives;
    public RequirementDict backupRequirementDict; // Used if requirement dict is empty
    public StringSettingDictionary requirementDict;
    private StoryEventChoiceDictionary _eventsFired;
    private EventManager eventManager;
    private GameState _gameState;

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
    public List<string> objectives
    {
        get { return _objectives; }
        set { _objectives = value; }
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
        }
    }

    private void Start()
    {
        //TODO: implement proper state flow
        gameState = GameState.investigator;
        _eventsFired = new StoryEventChoiceDictionary();
    }

    public void FireEvent() {
        StoryEvent eventFired = eventManager.InitializeEvent(); //fires first suitable event
        if (eventFired)
        {
            gameState = GameState.eventhandler;
            _eventsFired.Add(eventFired, null);
        }
        
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

        if (choice.endGameTrigger)
        {
            SceneManager.LoadScene("TEMP_EndgameScene");
        }
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
        
        gameState = GameState.investigator;
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    
}
