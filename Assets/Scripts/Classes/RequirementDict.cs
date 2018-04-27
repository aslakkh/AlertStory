using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "Test", menuName = "Alert/RequirementDictTest", order = 1)]

public class RequirementDict : ScriptableObject {
    //bool needs to be exact in Compared Dictionary
    public RequirementToIntDictionary requirementDictionary = new RequirementToIntDictionary();
    
    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementDict requirementList) {
        foreach(KeyValuePair<Requirement, int> item in requirementList.requirementDictionary) {
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value)) {
                return false;
            }
        }
        return true;
    }
    
    public void Add(Requirement requirement, int value) {
         requirementDictionary.Add(requirement, value);   
    }
    
    public void Remove(Requirement requirement) {
        requirementDictionary.Remove(requirement);
    }

    public void UpdateValue(Requirement requirement, int newValue) {
        if (requirementDictionary.ContainsKey(requirement)) {
            requirementDictionary[requirement] = newValue;
        }
    }
}
