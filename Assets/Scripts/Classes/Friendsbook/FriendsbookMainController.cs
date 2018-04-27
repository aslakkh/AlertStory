using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsbookMainController : MonoBehaviour {

    public GameObject personViewPrefab;
    public GameObject playerViewPrefab;

    private GameObject personView;

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Assert(gameManager != null, "FriendsbookMainController could not find GameManager...");
        EnterPlayerCharacterProfile();
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
        p.transform.SetAsFirstSibling(); //assures that this is rendered before overlay elements
        p.GetComponent<FriendsbookPlayerController>().SetCharacter(gameManager.playerCharacter);
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
        p.transform.SetAsFirstSibling(); //assures that this is rendered before overlay elements
        p.GetComponent<FriendsbookPersonController>().SetCharacter(c);
        p.GetComponent<FriendsbookPersonController>().SetFriendsbookMainController(this);
    }

    public void AddFriendship(Character c)
    {
        gameManager.playerCharacter.friendsbookProfile.AddFriend(c);
        c.friendsbookProfile.AddFriend(gameManager.playerCharacter);
    }

    public bool PlayerIsFriendsWith(Character c)
    {
        return (gameManager.playerCharacter.friendsbookProfile.IsFriendWith(c));
    }
}
