using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryEventController : EventController {
    public BinaryEventView eventView;

	// Use this for initialization
	void Start () {
        //Set UI elements
        eventView.SetEventTitle(_storyEvent._title);
        eventView.SetEventDescription(_storyEvent._text);
        eventView.SetAffirmativeButtonText("Test");
        eventView.SetDissentiveButtonText("NOPE");

        //TODO: set choice descriptions
    }

}
