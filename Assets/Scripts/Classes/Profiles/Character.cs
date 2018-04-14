using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : ScriptableObject {

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



}
