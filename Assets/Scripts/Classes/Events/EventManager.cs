using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private GameManager gameManager;

    public GameObject binaryChoicePrefab;
    public GameObject informativeEventPrefab;

    //public List<StoryEvent> storyEvents;
    public StoryEventList storyEvents;

    private List<StoryEvent> storyEventsInternal;

    //Singleton instanciating
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        //Check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }
        // Here we save our singleton instance
        Instance = this;

        // Makes sure that we don't destroy between scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (storyEvents != null)
        {
            storyEventsInternal = new List<StoryEvent>(storyEvents.list);
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    //finds first relevant event, instantiates prefab, and assigns event to prefab. 
    //Returns true if an event was initialized, false otherwise
    public StoryEvent InitializeEvent() 
    {
        StoryEvent storyEvent = FindFirstRelevantEvent(storyEventsInternal);
        if(storyEvent != null)
        {
            Transform phoneMainScreen = GameObject.Find("PhoneMainScreen").transform;
            GameObject eventObject;
            //check whether storyEvent is multiple choice, and instantiate correct prefab
            if (storyEvent.IsMultipleChoice())
            {
                eventObject = Instantiate(binaryChoicePrefab, phoneMainScreen);
            }
            else
            {
                eventObject = Instantiate(informativeEventPrefab, phoneMainScreen);
            }

            EventController eventController = eventObject.GetComponent<EventController>();
            eventController.storyEvent = storyEvent;

            return storyEvent;
        }
        else
        {
            Debug.Log("No relevant storyevents can be fired. ", this);
            return null;
        }
        
    }

    public StoryEvent FindFirstRelevantEvent(List<StoryEvent> storyEventList)
    {
        StoryEvent e = null;
        for(int i = 0; i < storyEventList.Count; i++)
        {
            if(CanBeFired(storyEventList[i]))
            {
                e = storyEventList[i];
                storyEventList.RemoveAt(i);
                break;
            }
        }
        return e;
    }

    private bool CanBeFired(StoryEvent e) //checks if e fits requirements and dependencies in gamemanager
    {
        return (e.requirements.FitsRequirements(gameManager.requirements) && e.dependencies.FitsRequirements(gameManager.eventsFired));
        //turn true;
    }

}
