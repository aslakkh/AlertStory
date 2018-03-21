using System.Collections.Generic;

public class Dependencies {
    //Made public so they can later be interacted with in unity editor script.
    public Dictionary<StoryEvent, bool> dependenciesDict;

    //Loops each element in remote dict, if local dict also has the same key and their values doesn't match return false
    public bool FitsRequirements(List<StoryEvent> eventList) {
        foreach (KeyValuePair<StoryEvent, bool> item in this.dependenciesDict) {
            if (eventList.Contains(item.Key) != item.Value) {
                return false;
            }
        }
        return true;
    }
}
