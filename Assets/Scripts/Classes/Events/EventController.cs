using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EventController : MonoBehaviour {

    //public static GameObject eventPrefab;

    private StoryEvent _storyEvent;
    public StoryEvent storyEvent
    {
        get { return _storyEvent; }
        set { _storyEvent = value; }
    }

    private void Awake()
    {
        //eventPrefab = Resources.Load("BinaryChoiceEvent") as GameObject;
    }

    //public static void CreateEvent(StoryEvent storyEvent)
    //{
    //    Transform phoneMainScreen = GameObject.Find("PhoneMainScreen").transform;
    //    GameObject e = Instantiate(EventController.eventPrefab, phoneMainScreen);
    //    EventController eventController = e.GetComponent<EventController>();
    //    eventController._storyEvent = storyEvent;
    //}

    //private void Start()
    //{
    //    //Set UI elements
    //    eventView.SetEventTitle(_storyEvent._title);
    //    eventView.SetEventDescription(_storyEvent._text);
    //    eventView.SetAffirmativeButtonText("Test");
    //    eventView.SetDissentiveButtonText("NOPE");

    //    //TODO: set choice descriptions
    //}

    public void DestroyFromScene()
    {
        Destroy(gameObject);
    }
}
