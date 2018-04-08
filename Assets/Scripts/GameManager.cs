using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int score;
    public int privateScore;
    public int dayCount;
    public int turnCount;
    private RequirementDict requirementDict;
    private List<StoryEvent> _eventsFired;

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
    }

    public void FireEvent() {
        //Find list of events and fire first suitable.??
    }
    
}
