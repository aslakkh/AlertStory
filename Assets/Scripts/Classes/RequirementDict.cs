using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Settings;

[System.Serializable]
public class RequirementDict : ScriptableObject {
    //bool needs to be exact in Compared Dictionary
    public StringSettingDictionary requirementDictionary = new StringSettingDictionary();
    
    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementDict remoteDict) {
        if(this.requirementDictionary.Count == 0 || remoteDict.requirementDictionary.Count == 0) { return true; }
        foreach(KeyValuePair<string, Setting> item in remoteDict.requirementDictionary) {
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value)) {
                return false;
            }
        }
        return true;
    }

    public bool FitsRequirements(StringSettingDictionary remoteDict)
    {
        if (this.requirementDictionary.Count == 0 || remoteDict.Count == 0) { return true; }
        foreach (KeyValuePair<string, Setting> item in remoteDict)
        {
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value))
            {
                return false;
            }
        }
        return true;
    }

    public void Add(string requirement, Setting value) {
         requirementDictionary.Add(requirement, value);   
    }
    
    public void Remove(string requirement) {
        requirementDictionary.Remove(requirement);
    }

    public void UpdateValue(string requirement, Setting newValue) {
        if (requirementDictionary.ContainsKey(requirement)) {
            requirementDictionary[requirement] = newValue;
        }
    }


}
