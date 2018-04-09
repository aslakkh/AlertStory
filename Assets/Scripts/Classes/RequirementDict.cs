﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RequirementDict {
    //bool needs to be exact in Compared Dictionary
    public Dictionary<Requirement, bool> requirementDictionary;
    
    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementDict requirementList) {
        foreach(KeyValuePair<Requirement, bool> item in requirementList.requirementDictionary) {
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value)) {
                return false;
            }
        }
        return true;
    }
    
    public void Add(Requirement requirement, bool value) {
         requirementDictionary.Add(requirement, value);   
    }
    
    public void Remove(Requirement requirement) {
        requirementDictionary.Remove(requirement);
    }

    public void Update(Requirement requirement, bool newValue) {
        if (requirementDictionary.ContainsKey(requirement)) {
            requirementDictionary[requirement] = newValue;
        }
    }
}