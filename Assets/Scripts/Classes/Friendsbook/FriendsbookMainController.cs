using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsbookMainController : MonoBehaviour {

    //Friendsbook friends view:
    //- display list of 20-25 friends, with a next/prev button?
    //- include search through friends?

	// Use this for initialization
	void Start () {
		
	}
	
	public void EnterFriendsbookProfile(Character c)
    {
        Debug.Log(c.fullName);
    }
}
