using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FriendsbookPostList : ScriptableObject
{
    public List<FriendsbookPost> list;

    public void Init()
    {
        list = new List<FriendsbookPost>();
    }


    public FriendsbookPost this[int i]
    {
        get { return list[i]; }
    }

    public int Count
    {
        get { return list.Count; }
    }

    public void RemoveAt(int i)
    {
        list.RemoveAt(i);
    }

    public void Add(FriendsbookPost post)
    {
        list.Add(post);
    }
}
