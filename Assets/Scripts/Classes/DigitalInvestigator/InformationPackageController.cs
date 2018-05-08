using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPackageController : MonoBehaviour {

	private GameManager gameManager;
	private Dictionary<int, Objective> objectives;

	void Awake() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		objectives = gameManager.objectives;
		Debug.Log("Objectives: " + objectives.ToString());
	}

	// Validates the information gathered by the user after the user has delivered the information
	// or time is up
	public void ValidateInformationGathered() {
		//Do something
	}

}
