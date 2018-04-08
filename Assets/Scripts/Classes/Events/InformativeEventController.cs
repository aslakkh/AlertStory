using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformativeEventController : EventController {
    public InformativeEventView eventView;

    // Use this for initialization
    void Start()
    {
        //Set UI elements
        eventView.SetEventTitle(storyEvent._title);
        eventView.SetEventDescription(storyEvent._text);
        eventView.SetButtonText(storyEvent._choices[0]._choiceDescription);
    }

    public void HandleChoice()
    {
        //add the score values of this._choices[0] to gamemanager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SetScore(storyEvent._choices[0]._affectScore);
        gameManager.SetPrivateScore(storyEvent._choices[0]._affectSecretScore);
        DestroyFromScene();
    }
}
