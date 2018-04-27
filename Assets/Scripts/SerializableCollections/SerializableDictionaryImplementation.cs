using System;
using SerializableCollections;

[Serializable]
public class RequirementToSettingDictionary : SerializableDictionary<string, Settings.Setting> {
    
}

[Serializable]
public class StoryEventToBoolDictionary : SerializableDictionary<StoryEvent, bool> {
    
}