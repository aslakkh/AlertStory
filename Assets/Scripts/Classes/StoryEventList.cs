using UnityEngine;
using System.Collections.Generic;


public class StoryEventList : ScriptableObject {
    public List<StoryEvent> list;

    public int GetCount()
    {
        return list.Count;
    }

    public StoryEvent this[int itemIndex]
    {
        get { return list[itemIndex]; }
    }

    public void RemoveElement(int i)
    {
        list.RemoveAt(i);
    }
}