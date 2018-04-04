using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public GameObject binaryChoicePrefab;
    public GameObject informativeEventPrefab;

    public StoryEvent storyEvent;

    private void Awake()
    {

    }

    public void InitializeEvent()
    {
        Transform phoneMainScreen = GameObject.Find("PhoneMainScreen").transform;
        GameObject e;
        //check whether storyEvent is multiple choice, and instantiate correct prefab
        if (storyEvent.IsMultipleChoice())
        {
            e = Instantiate(binaryChoicePrefab, phoneMainScreen);
        }
        else
        {
            e = Instantiate(informativeEventPrefab, phoneMainScreen);
        }
        
        EventController eventController = e.GetComponent<EventController>();
        eventController.storyEvent = storyEvent;



        //EventController.CreateEvent(storyEvent);
    }
}
