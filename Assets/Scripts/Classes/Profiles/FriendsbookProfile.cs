using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FriendsbookProfile : ScriptableObject {

    public Character character; //the character this profile belongs to
    [SerializeField]
    public List<Character> friends;

    //should also include Facebook specific info, posts, etc...

    public void Init(Character c)
    {
        character = c;
        friends = new List<Character>();
    }

    public void AddFriend(Character c)
    {
        if(c != null)
        {
            if (!friends.Contains(c))
            {
                friends.Add(c);
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

}
