using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[CreateAssetMenu(fileName = "Test", menuName = "Alert/DependenciesTest", order = 1)]

public class Dependencies : ScriptableObject {
    //Made public so they can later be interacted with in unity editor script.
    public StoryEventBoolDictionary dependenciesDict = new StoryEventBoolDictionary();

    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(List<StoryEvent> eventList) {
        foreach (KeyValuePair<StoryEvent, bool> item in this.dependenciesDict) {
            if (eventList.Contains(item.Key) != item.Value) {
                return false;
            }
        }
        return true;
    }
    
    public void Add(StoryEvent storyEvent, bool value) {
        dependenciesDict.Add(storyEvent, value);   
    }
    
    public void Remove(StoryEvent storyEvent) {
        dependenciesDict.Remove(storyEvent);
    }

    public void UpdateValue(StoryEvent storyEvent, bool newValue) {
        if (dependenciesDict.ContainsKey(storyEvent)) {
            dependenciesDict[storyEvent] = newValue;
        }
    }
}
