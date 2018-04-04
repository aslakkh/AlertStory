using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformativeEventController : EventController {
    public InformativeEventView eventView;

    // Use this for initialization
    void Start()
    {
        //Set UI elements
        eventView.SetEventTitle(_storyEvent._title);
        eventView.SetEventDescription(_storyEvent._text);
        eventView.SetButtonText("TESTING");

        //TODO: set choice descriptions
    }
}
