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
        if(args.newState == GameState.investigator)
        {
            SetInteractable(true);
        }
        else //buttons should only be interactable in investigator state
        {
            SetInteractable(false);
        }
    }

    public void SetInteractable(bool interactable)
    {
        foreach (Button b in buttons)
        {
            b.interactable = interactable;
        }
    }

    private void Start()
    {
        buttons[0].onClick.AddListener(delegate () { gameManager.FireEvent(); });
    }

}
