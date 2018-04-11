using UnityEngine;
using System.Collections.Generic;

public class RequirementList : MonoBehaviour {
    //bool needs to be exact in Compared Dictionary
    public Dictionary<Requirement, int> requirementDictionary;
    
    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(RequirementList requirementList) {
        foreach(KeyValuePair<Requirement, int> item in requirementList.requirementDictionary) {
            if (this.requirementDictionary.ContainsKey(item.Key) && (this.requirementDictionary[item.Key] != item.Value)) {
                return false;
            }
        }
        return true;
    }
}
