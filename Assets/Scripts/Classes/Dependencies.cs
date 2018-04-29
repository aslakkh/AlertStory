using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct StoryDependencyBool
{
    public bool fired; //dependent on being fired
    public bool choiceA; //true if dependent on choice A
    public bool choiceB;

    public StoryDependencyBool(bool fired, bool choiceA, bool choiceB)
    {
        this.fired = fired;
        this.choiceA = choiceA;
        this.choiceB = choiceB;
    }

}

[System.Serializable]
[CreateAssetMenu(fileName = "Test", menuName = "Alert/DependenciesTest", order = 1)]
public class Dependencies : ScriptableObject {
    //Made public so they can later be interacted with in unity editor script.
    public StoryEventBoolDictionary dependenciesDict = new StoryEventBoolDictionary();

    //Loops each element in remote dict, checks if bools in local dict match choice in remote dict
    public bool FitsRequirements(StoryEventChoiceDictionary remoteDict) {
        foreach (KeyValuePair<StoryEvent, StoryDependencyBool> item in this.dependenciesDict) {
            if (remoteDict.ContainsKey(item.Key) != item.Value.fired) { //checks fired-status of event matches dependencies
                return false;
            }
            else if (remoteDict.ContainsKey(item.Key)) //event has been fired
            {
                if(item.Key.choices.Count == 2){ //only care about choice taken if there were two choices
                    if (item.Value.choiceA) 
                    {
                        //item is dependent on choice A, check that choice A was taken
                        if (remoteDict[item.Key] != item.Key.choices[0])
                        {
                            return false;
                        }
                    }
                    else if(item.Value.choiceB)
                    {
                        //item is dependent on choice B, check that choice B was taken
                        if (remoteDict[item.Key] != item.Key.choices[1])
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    
    public void Add(StoryEvent storyEvent, StoryDependencyBool value) {
        dependenciesDict.Add(storyEvent, value);   
    }
    
    public void Remove(StoryEvent storyEvent) {
        dependenciesDict.Remove(storyEvent);
    }

    public void UpdateValue(StoryEvent storyEvent, StoryDependencyBool newValue) {
        if (dependenciesDict.ContainsKey(storyEvent)) {
            dependenciesDict[storyEvent] = newValue;
        }
    }

}
