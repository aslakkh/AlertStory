using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryEventController : EventController {
    public BinaryEventView eventView;

	// Use this for initialization
	void Start () {
        //Set UI elements
        eventView.SetEventTitle(storyEvent.title);
        eventView.SetEventDescription(storyEvent.text);
        eventView.SetAffirmativeButtonText(storyEvent.choices[0].choiceDescription);
        eventView.SetDissentiveButtonText(storyEvent.choices[1].choiceDescription);
    }

    public void HandleChoice(bool affirmative) //called on affirmative button or dissentive button push
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //TODO: USE MESSAGING INSTEAD
        Choice c = affirmative ? storyEvent.choices[0] : storyEvent.choices[1];
        gameManager.SetScore(c.affectScore);
        gameManager.SetPrivateScore(c.affectSecretScore);
        DestroyFromScene();
    }
}
