using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryEventList", menuName = "Alert/StoryEventList", order = 1)]
public class StoryEventList : ScriptableObject {

    public List<StoryEvent> storyEventList;
}
