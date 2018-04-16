using UnityEngine;
using System.Collections.Generic;

public class StoryEventList : ScriptableObject {
       public List<StoryEvent> list;

    public void Init()
    {
        list = new List<StoryEvent>();
    }
}