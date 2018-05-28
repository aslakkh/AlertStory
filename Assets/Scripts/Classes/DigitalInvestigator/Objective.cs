using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Objective {

    // The task displayed to the player
    public string description;
    
    
    //Contains personal info for target person
    public List<string> tasks;
    public Objective (string desc, List<string> tasks) {
        this.description = desc;
        this.tasks = tasks;
    }

}
