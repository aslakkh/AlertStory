using System;
using SerializableCollections;

[Serializable]
public class RequirementToIntDictionary : SerializableDictionary<Requirement, int> {
    
}

[Serializable]
public class StoryEventToBoolDictionary : SerializableDictionary<StoryEvent, bool> {
    
}
