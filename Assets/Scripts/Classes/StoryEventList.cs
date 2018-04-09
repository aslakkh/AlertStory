using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "StoryEventList", menuName = "Alert/StoryEventList", order = 1)]
public class StoryEventList : ScriptableObject {
    public List<StoryEvent> list;

    public int GetCount()
    {
        return list.Count;
    }

    public StoryEvent GetElement(int i)
    {
        return list[i];
    }

    public void RemoveElement(int i)
    {
        list.RemoveAt(i);
    }
}