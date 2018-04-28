using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StoryEventBoolDictionary : SerializableDictionary<StoryEvent, bool> {}

[Serializable]
public class StringSettingDictionary : SerializableDictionary<string, Settings.Setting> {}