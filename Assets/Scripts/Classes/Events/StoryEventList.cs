using UnityEngine;
using System;
using System.Collections.Generic;

public class StoryEventList : ScriptableObject
{
    public List<StoryEvent> list = new List<StoryEvent>();

    public void Add(StoryEvent s)
    {
        if (!list.Contains(s))
        {
            list.Add(s);
        }
        else
        {
            Debug.Log("Tried adding duplicate item to StoryEventList", this);
        }
    }

    public void RemoveAt(int i)
    {
        try
        {
            list.RemoveAt(i);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.Log(e.Message);
        }
    }

    public void Swap(int indexA, int indexB)
    {
        try
        {
            StoryEvent temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.Log(e.Message);
        }
    }
}