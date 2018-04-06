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
        eventView.SetAffirmativeButtonText(storyEvent._choices[0]._choiceDescription);
        eventView.SetDissentiveButtonText(storyEvent._choices[1]._choiceDescription);
    }

    public void HandleChoice(bool affirmative) //called on affirmative button or dissentive button push
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //TODO: USE MESSAGING INSTEAD
        Choice c = affirmative ? storyEvent._choices[0] : storyEvent._choices[1];
        gameManager.SetScore(c._affectScore);
        gameManager.SetPrivateScore(c._affectSecretScore);
        DestroyFromScene();
    }
}
