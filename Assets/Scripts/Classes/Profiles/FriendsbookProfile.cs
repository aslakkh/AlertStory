using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;

public class FriendsbookProfile : ScriptableObject {

    public Character character; //the character this profile belongs to
    [SerializeField]
    public List<Character> friends;
    public FriendsbookPostList posts;

    //settings
    public Setting informationSetting;
    public Setting friendsSetting;
    public Setting postsSetting;

    public void Init(Character c)
    {
        character = c;
        friends = new List<Character>();
        UseStandardSettings(); //init with standard settings, defined in Settings namespace. Can be overwritten
    }

    public void AddFriend(Character c)
    {
        if(c != null)
        {
            if (!friends.Contains(c))
            {
                friends.Add(c);
                friends.Sort((x, y) => x.CompareTo(y)); //sort alphabetical order
            }
            else
            {
                Debug.Log("Tried adding duplicate friendsbook friend " + c.fullName, this);
            }
        }
        
        
    }

    public void RemoveFriend(Character c)
    {
        if (friends.Contains(c))
        {
            friends.Remove(c);
        }
        else
        {
            Debug.Log(string.Format("Tried removing nonexisting friendship between {0} and {1}", character.fullName, c.fullName), this);
        }
    }

    public bool HasFriends()
    {
        return friends.Count > 0;
    }

    public bool HasPosts()
    {
        if(posts.list != null)
        {
            return posts.list.Count > 0;
        }
        else
        {
            Debug.LogError("No post list initiated", this);
            return false;
        }
        
    }

    public void UseStandardSettings()
    {
        informationSetting = StandardFriendsbookSettings.informationSetting;
        friendsSetting = StandardFriendsbookSettings.friendsSetting;
        postsSetting = StandardFriendsbookSettings.postsSetting;
    }

}
