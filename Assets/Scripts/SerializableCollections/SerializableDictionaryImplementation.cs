using System;
using SerializableCollections;

[Serializable]
public class RequirementToSettingDictionary : SerializableDictionary<Requirement, Settings.Setting> {
    
}

[Serializable]
public class StoryEventToBoolDictionary : SerializableDictionary<StoryEvent, bool> {
    
}