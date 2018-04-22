using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : ScriptableObject, IComparable<Character> {

    //reference to characterList this character belongs to
    public CharacterList characterList;

    //General info
    public string fullName;
    public string address;
    public string email;
    public string phoneNumber;

    //Profiles
    public FriendsbookProfile friendsbookProfile;
    //public PhotochatProfile photochatProfile;

    public void Init(CharacterList c, string fullName)
    {
        characterList = c;
        this.fullName = fullName;
    }

    public bool hasFriendsbookProfile()
    {
        return (friendsbookProfile != null);
    }

    public List<Character> GetFriendsbookFriends()
    {
        if (hasFriendsbookProfile())
        {
            if (friendsbookProfile.friends != null) { return friendsbookProfile.friends; }
            else
            {
                Debug.Log(string.Format("Character {0} has no Friendsbook friends.", fullName), this);
                return null;
            }
        }
        else
        {
            Debug.Log(string.Format("Tried getting FriendsbookFriends from character {0}, which has no friendsbook profile.", fullName), this);
            return null;
        }
    }

    public int CompareTo(Character that)
    {
        return fullName.CompareTo(that.fullName); 
    }
}
