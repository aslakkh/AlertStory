using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FriendsbookProfile", menuName = "Alert/FriendsbookProfile", order = 1)]
//[System.Serializable]
public class FriendsbookProfile : ScriptableObject {

    //public Character character;
    public List<Character> friends;

    //should also include Facebook specific info, posts, etc...

    public void Init()
    {
        friends = new List<Character>();
    }

}
