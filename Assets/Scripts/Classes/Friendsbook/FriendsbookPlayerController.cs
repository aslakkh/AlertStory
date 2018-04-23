using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookPlayerController : MonoBehaviour {

    public GameObject friendsViewPrefab;
    public ScrollRect scrollRect;

    private Character character;
    private GameObject currentView;
    

	// Use this for initialization
	void Start () {
        currentView = Instantiate(friendsViewPrefab);
        currentView.transform.SetParent(scrollRect.content, false);
        var friendsbookFriendsController = currentView.GetComponent<FriendsbookFriendsController>();
        friendsbookFriendsController.SetFriends(character.friendsbookProfile.friends);
    }
	
	public void SetCharacter(Character c) { character = c; }
}
