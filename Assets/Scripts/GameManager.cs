using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int score;
    public int privateScore;
    public int dayCount;
    public int turnCount;
    private RequirementList requirementList;

    public RequirementList requirements {
        get { return requirementList; }
        set { requirementList = value; }
    }

    private List<StoryEvent> _eventsFired;

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
    
}
