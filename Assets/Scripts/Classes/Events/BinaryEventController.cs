using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryEventController : EventController {
    public BinaryEventView eventView;

	// Use this for initialization
	void Start () {
        //Set UI elements
        eventView.SetEventTitle(storyEvent._title);
        eventView.SetEventDescription(storyEvent._text);
        //eventView.SetAffirmativeButtonText("Test");
        //eventView.SetDissentiveButtonText("NOPE");

        //TODO: set choice descriptions
        eventView.SetAffirmativeButtonText(storyEvent._choices[0]._choiceDescription);
        eventView.SetDissentiveButtonText(storyEvent._choices[1]._choiceDescription);
    }

}
