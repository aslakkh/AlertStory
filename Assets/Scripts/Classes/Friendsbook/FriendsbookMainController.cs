using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsbookMainController : MonoBehaviour {

    public GameObject personViewPrefab;

    private GameObject personView;

    //Friendsbook friends view:
    //- display list of 20-25 friends, with a next/prev button?
    //- include search through friends?

	// Use this for initialization
	void Start () {
		
	}
	
	public void EnterFriendsbookProfile(Character c)
    {
        if(personView != null)
        {
            GameObject.Destroy(personView);
        }
        //Debug.Log(c.fullName);
        GameObject p = Instantiate(personViewPrefab);
        personView = p;
        p.transform.SetParent(transform, false);
        p.transform.SetSiblingIndex(1); //ui rendering position
        p.GetComponent<FriendsbookPersonController>().SetCharacter(c);
    }
}
