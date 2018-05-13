using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformativeEventController : EventController {
    public InformativeEventView eventView;

    // Use this for initialization
    void Start()
    {
        //Set UI elements
        if(storyEvent != null)
        {
            eventView.SetEventTitle(storyEvent.title);
            eventView.SetEventDescription(storyEvent.text);
            eventView.SetButtonText(storyEvent.choices[0].choiceDescription);
        }
        
    }

    public void HandleChoice()
    {
        //add the score values of this.choices[0] to gamemanager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (storyEvent != null)
        {
            gameManager.HandleChoice(storyEvent, storyEvent.choices[0]);
        }
        else //event is dummy event, no choice and storyevent information needs to be recorded
        {
            gameManager.HandleChoice();
        }
        
        DestroyFromScene();
    }
}
