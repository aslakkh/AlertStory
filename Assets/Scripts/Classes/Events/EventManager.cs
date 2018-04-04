using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private GameManager gameManager;

    public GameObject binaryChoicePrefab;
    public GameObject informativeEventPrefab;

    public StoryEventModel storyEventModel;
    public List<StoryEvent> storyEvents;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void InitializeEvent()
    {
        StoryEvent storyEvent = FindFirstRelevantEvent(); //TODO: Find first relevant

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



        //EventController.CreateEvent(storyEvent);
    }

    public StoryEvent FindFirstRelevantEvent()
    {
        StoryEvent e = null;
        for(int i = 0; i < storyEvents.Count; i++)
        {
            if(CanBeFired(storyEvents[i]))
            {
                e = storyEvents[i];
                storyEvents.RemoveAt(i);
                break;
            }
        }
        return e;
    }

    private bool CanBeFired(StoryEvent e) //return if e fits requirements and dependencies in gamemanager
    {
        //return (e.requirements.FitsRequirements(gameManager.requirements) && e.dependencies.FitsRequirements(gameManager.eventsFired));
        return true;
    }

}
