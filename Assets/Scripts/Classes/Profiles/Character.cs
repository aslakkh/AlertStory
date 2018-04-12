using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : ScriptableObject {

    public string firstName;
    public string lastName;
    public string address;
    public string email;
    public string phoneNumber;

    public FriendsbookProfile friendsbookProfile;
    //public PhotochatProfile photochatProfile;

    public void Init(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public void AddFriendsbookFriend(Character c)
    {
        if (!friendsbookProfile.friends.Contains(c)) //uses linear search (O(n))
        {
            friendsbookProfile.friends.Add(c);
            c.friendsbookProfile.friends.Add(this);
        }
        else
        {
            Debug.Log("Tried adding duplicate FriendsbookFriend. ", this);
        }
        
    }

}
