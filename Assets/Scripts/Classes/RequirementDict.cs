using UnityEngine;
using System.Collections.Generic;
using Settings;
[System.Serializable]
public class RequirementDict {
    //bool needs to be exact in Compared Dictionary
<<<<<<< HEAD
    public Dictionary<Requirement, bool> requirementDictionary;
    
//Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementDict requirementList) {
        foreach(KeyValuePair<Requirement, bool> item in requirementList.requirementDictionary) {
=======
    public Dictionary<Requirement, Setting> requirementDictionary = new Dictionary<Requirement, Setting>();
    
    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementDict requirementList) {
        foreach(KeyValuePair<Requirement, Setting> item in requirementList.requirementDictionary) {
>>>>>>> d73e6e23e49ab48d2591c539e4fa728e4dc9fca9
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value)) {
                return false;
            }
        }
        return true;
    }

    public void Add(Requirement requirement, Setting sett) {
         requirementDictionary.Add(requirement, sett);
    }
    
    public void Remove(Requirement requirement) {
        requirementDictionary.Remove(requirement);
    }

    public void Update(Requirement requirement, Setting sett) {
        if (requirementDictionary.ContainsKey(requirement)) {
            requirementDictionary[requirement] = sett;
        }
    }
}
