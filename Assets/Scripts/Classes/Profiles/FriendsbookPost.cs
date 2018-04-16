using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FriendsbookPost
{
    public string content;
    public DateTime date;
    public FriendsbookProfile to;
    public FriendsbookProfile from;

    public FriendsbookPost()
    {
        content = "";
        date = new DateTime();
    }

    public FriendsbookPost(string content, DateTime date, FriendsbookProfile to, FriendsbookProfile from)
    {
        this.content = content;
        this.date = date;
        this.to = to;
        this.from = from;
    }
}
