using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private GameManager gameManager;

    public GameObject binaryChoicePrefab;
    public GameObject informativeEventPrefab;

    //public List<StoryEvent> storyEvents;
    public StoryEventList storyEvents;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void InitializeEvent() //finds first relevant event, instantiates prefab, and assigns event to prefab
    {
        StoryEvent storyEvent = FindFirstRelevantEvent();
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
        }
        else
        {
            Debug.Log("No relvant storyevents can be fired. ", this);
        }
        
    }

    public StoryEvent FindFirstRelevantEvent()
    {
        StoryEvent e = null;
        for(int i = 0; i < storyEvents.GetCount(); i++)
        {
            if(CanBeFired(storyEvents.GetElement(i)))
            {
                e = storyEvents.GetElement(i);
                storyEvents.RemoveElement(i);
                break;
            }
        }
        return e;
    }

    private bool CanBeFired(StoryEvent e) //checks if e fits requirements and dependencies in gamemanager
    {
        //return (e.requirements.FitsRequirements(gameManager.requirements) && e.dependencies.FitsRequirements(gameManager.eventsFired));
        return true;
    }

}
