using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {
    // The task displayed to the player
    public string description;
    
    //Tags used to check informationpackage
    private List<string> tags;
    
    //Contains personal info for target person
    public List<string> tasks;
}
