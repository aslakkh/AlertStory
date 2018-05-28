using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private GameManager gameManager;

    public GameObject binaryChoicePrefab;
    public GameObject informativeEventPrefab;
    public GameObject batteryDepletionEventPrefab;
    public GameObject informationPackageSentEventPrefab;
    public GameObject messagePrefab;

    //public List<StoryEvent> storyEvents;
    public StoryEventList storyEvents;

    private List<StoryEvent> storyEventsInternal;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (storyEvents != null)
        {
            storyEventsInternal = new List<StoryEvent>(storyEvents.list);
        }
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
                if (storyEvent.isMessage)
                {
                    eventObject = Instantiate(messagePrefab, phoneMainScreen);
                }
                else
                {
                    eventObject = Instantiate(informativeEventPrefab, phoneMainScreen);
                }
               
            }

            EventController eventController = eventObject.GetComponent<EventController>();
            eventController.storyEvent = storyEvent;

            return storyEvent;
        }
        else
        {
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

    //instantiates battery depletion event (from prefab) 
    public void InstantiateBatteryDepletionEvent()
    {
        Transform phoneMainScreen = GameObject.Find("PhoneMainScreen").transform;
        GameObject eventObject = Instantiate(batteryDepletionEventPrefab);
        eventObject.transform.SetParent(phoneMainScreen, false);
    }

    //instantiates battery depletion event (from prefab) 
    public void InstantiateInformationPackageSentEvent()
    {
        Transform phoneMainScreen = GameObject.Find("PhoneMainScreen").transform;
        GameObject eventObject = Instantiate(informationPackageSentEventPrefab);
        eventObject.transform.SetParent(phoneMainScreen, false);
    }

}
