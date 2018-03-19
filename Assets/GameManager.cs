using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GameManager : MonoBehaviour {
    public int day;
    public int turn;
    public int apps;

	// Use this for initialization
	void Start () {
        apps = 1;
		if (apps != null && apps > 0)
        {
            Debug.Log("Entered if loop");
        }
        else
        {
            Debug.Log("Entered else loop");
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
