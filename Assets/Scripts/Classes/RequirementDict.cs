using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Settings;

[System.Serializable]
[CreateAssetMenu(fileName = "Test", menuName = "Alert/RequirementDictTest", order = 1)]
public class RequirementDict : ScriptableObject {
    //bool needs to be exact in Compared Dictionary
    public RequirementSettingDictionary requirementDictionary = new RequirementSettingDictionary();
    
    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementDict requirementList) {
        if(this.requirementDictionary.Count == 0 || requirementList.requirementDictionary.Count == 0) { return true; }
        foreach(KeyValuePair<Requirement, Setting> item in requirementList.requirementDictionary) {
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value)) {
                Debug.Log("Hey");
                return false;
            }
        }
        return true;
    }
    public void Add(Requirement requirement, Setting value) {
         requirementDictionary.Add(requirement, value);   
    }
    
    public void Remove(Requirement requirement) {
        requirementDictionary.Remove(requirement);
    }

    public void UpdateValue(Requirement requirement, Setting newValue) {
        if (requirementDictionary.ContainsKey(requirement)) {
            requirementDictionary[requirement] = newValue;
        }
    }
}
