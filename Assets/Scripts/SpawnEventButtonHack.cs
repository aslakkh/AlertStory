using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEventButtonHack : MonoBehaviour {
    GameManager gameManager;

	// Use this for initialization
	void Awake () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void HandleClick()
    {
        gameManager.FireEvent();
    }
}
