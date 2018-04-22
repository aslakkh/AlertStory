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

    public int CompareTo(Character that)
    {
        return fullName.CompareTo(that.fullName); 
    }
}
