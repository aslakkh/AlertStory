using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StoryEventBoolDictionary : SerializableDictionary<StoryEvent, StoryDependencyBool> {}

[Serializable]
public class StringSettingDictionary : SerializableDictionary<string, Settings.Setting> {}

[Serializable]
public class StoryEventChoiceDictionary : SerializableDictionary<StoryEvent, Choice> { }