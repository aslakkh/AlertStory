using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
    public Character playerCharacter; //reference to scriptable object holding information about playercharacter
    public RequirementDict requirementDict;
    private List<StoryEvent> _eventsFired;
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
    public RequirementDict requirements {
        get { return requirementDict; }
        set { requirementDict = value; }
    }
    
    //Add all events that has fired here.
    public List<StoryEvent> eventsFired {
        get { return _eventsFired; }
        set { _eventsFired = value; }
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
    }

    private void Start()
    {
        //TODO: implement proper state flow
        gameState = GameState.investigator;
        _eventsFired = new List<StoryEvent>();
        Debug.Log(requirementDict.requirementDictionary.ElementAt(0));
    }

    public void FireEvent() {
        StoryEvent eventFired = eventManager.InitializeEvent(); //fires first suitable event
        if (eventFired)
        {
            gameState = GameState.eventhandler;
            _eventsFired.Add(eventFired);
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

    public void HandleChoice(int score, int privateScore)
    {
        AddToScore(score);
        AddToPrivateScore(score);
        gameState = GameState.investigator;
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    public void AddToPrivateScore(int value)
    {
        privateScore += value;
    }
    
}
