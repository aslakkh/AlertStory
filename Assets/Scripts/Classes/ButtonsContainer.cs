using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//add component to buttoncontainers to expose button interactability
public class ButtonsContainer : MonoBehaviour {

    public List<Button> buttons; //all buttons in this buttoncontainer

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.stateChanged += OnStateChanged; //subcribe to stateChanged event
    }

    public void OnStateChanged(object source, GameStateEventArgs args)
    {
        if(args.newState == GameState.investigator) //moving into investigator --> make interactable
        {
            SetInteractable(true);
        }
        else if(args.newState == GameState.eventhandler) //moving into eventhandler --> disable interactivity
        {
            SetInteractable(false);
        }
    }

    //set interactable status of all buttons contained within this
    public void SetInteractable(bool interactable)
    {
        foreach (Button b in buttons)
        {
            b.interactable = interactable;
        }
    }

    //remove subscriber from gamemanager
    private void OnDestroy()
    {
        gameManager.stateChanged -= OnStateChanged;
    }

}
