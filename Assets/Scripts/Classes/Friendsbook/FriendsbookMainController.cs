using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsbookMainController : MonoBehaviour {

    public GameObject personViewPrefab;
    public GameObject playerViewPrefab;

    private GameObject personView;

    private GameManager gameManager;

    //Friendsbook friends view:
    //- display list of 20-25 friends, with a next/prev button?
    //- include search through friends?

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Assert(gameManager != null, "FriendsbookMainController could not find GameManager...");
	}

    public void EnterPlayerCharacterProfile()
    {
        //EnterFriendsbookProfile(gameManager.playerCharacter);
        if (personView != null)
        {
            GameObject.Destroy(personView);
        }
        GameObject p = Instantiate(playerViewPrefab);
        personView = p;
        p.transform.SetParent(transform, false);
        p.transform.SetSiblingIndex(1); //ui rendering position
        p.GetComponent<FriendsbookPersonController>().SetCharacter(gameManager.playerCharacter);
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
