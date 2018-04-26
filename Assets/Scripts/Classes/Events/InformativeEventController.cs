using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformativeEventController : EventController {
    public InformativeEventView eventView;

    // Use this for initialization
    void Start()
    {
        //Set UI elements
        eventView.SetEventTitle(storyEvent.title);
        eventView.SetEventDescription(storyEvent.text);
        eventView.SetButtonText(storyEvent.choices[0].choiceDescription);
    }

    public void HandleChoice()
    {
        //add the score values of this.choices[0] to gamemanager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //gameManager.SetScore(storyEvent.choices[0].affectScore);
        //gameManager.SetPrivateScore(storyEvent.choices[0].affectSecretScore);
        DestroyFromScene();
    }
}
