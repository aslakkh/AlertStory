using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controller for Friendsbook's main view.
//displays either friendsbook homepage or profile page
//deals with friendsbook-related communication to gameManager (for adding/retrieving playerCharacter friends)
public class FriendsbookMainController : MonoBehaviour {

    public GameObject personViewPrefab; //prefab for character profiles view
    public GameObject playerViewPrefab; //prefab for player's main view (home)

    private GameObject currentView; //reference to view currently being displayed

    private GameManager gameManager;
    private SoundManager soundManager;

    // Use this for initialization
	void Start () {
        //Friendsbook main view is instantiated: find gamemanager and render friendsbook homepage
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	    soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        EnterPlayerCharacterProfile();
	}

    public void DestroyCurrentView()
    {
        soundManager.sfxSource.Play();
        if (currentView != null) //only allow one view at a time
        {
            GameObject.Destroy(currentView);
        }
    }


    //instantiates prefab, and sets this instance as currentview
    public GameObject InstantiateCurrentViewFromPrefab(GameObject prefab)
    {
        DestroyCurrentView();
        GameObject p = Instantiate(prefab);
        currentView = p;
        p.transform.SetParent(transform, false);
        p.transform.SetAsFirstSibling(); //assures that this is rendered before overlay elements
        return p;
    }

    //Sets currentView to friendsbook's homepage
    public void EnterPlayerCharacterProfile()
    {
        GameObject p = InstantiateCurrentViewFromPrefab(playerViewPrefab);
        p.GetComponent<FriendsbookPlayerController>().SetCharacter(gameManager.playerCharacter);
    }
	
    //Sets current view to profile page of character c
	public void EnterFriendsbookProfile(Character c)
    {
        GameObject p = InstantiateCurrentViewFromPrefab(personViewPrefab);
        p.GetComponent<FriendsbookPersonController>().SetCharacter(c);
        p.GetComponent<FriendsbookPersonController>().SetFriendsbookMainController(this);
    }

    //adds friendship relation between playerCharacter and Character c
    public void AddFriendship(Character c)
    {
        gameManager.playerCharacter.friendsbookProfile.AddFriend(c);
        c.friendsbookProfile.AddFriend(gameManager.playerCharacter);
    }

    //Check if there is a friendsbook friends-relation between playercharacter and character c
    public bool PlayerIsFriendsWith(Character c)
    {
        return (gameManager.playerCharacter.friendsbookProfile.IsFriendWith(c));
    }

    public GameManager GetGameManager()
    {
        return this.gameManager;
    }
}
