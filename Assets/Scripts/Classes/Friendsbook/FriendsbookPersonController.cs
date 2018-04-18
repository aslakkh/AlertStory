using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookPersonController : MonoBehaviour {

    public Text personName;
    public GameObject aboutViewPrefab;
    public GameObject friendsViewPrefab;
    public GameObject postsViewPrefab;
    public ScrollRect scrollRect;

    private GameObject currentView;
    private Character character;

	// Use this for initialization
	void Start () {
        SetPersonName();
        DisplayAboutView();
	}
	

    public void SetPersonName()
    {
        if(character != null)
        {
            personName.text = character.fullName;
        }
        else
        {
            Debug.LogError("FriendsbookPersonController missing character", this);
        }
        
    }

    //public void SetImage()
    //{

    //}

    public void SetCharacter(Character c) { character = c; }

    public void DisplayAboutView()
    {
        if(currentView != null)
        {
            GameObject.Destroy(currentView);
        }
        currentView = Instantiate(aboutViewPrefab);
        currentView.transform.SetParent(scrollRect.content, false);
        currentView.transform.localScale = Vector3.one;
        currentView.GetComponent<FriendsbookAboutController>().SetInformation(character);
    }

    public void DisplayPostsView()
    {
        if (currentView != null)
        {
            GameObject.Destroy(currentView);
        }
        currentView = Instantiate(postsViewPrefab);
        currentView.transform.SetParent(scrollRect.content, false);
        var postsController = currentView.GetComponent<FriendsbookPostsController>();
        postsController.SetPosts(character.friendsbookProfile.posts.list);
    }

    public void DisplayFriendsView()
    {
        if (currentView != null)
        {
            GameObject.Destroy(currentView);
        }
        currentView = Instantiate(friendsViewPrefab);
        currentView.transform.SetParent(scrollRect.content, false);
        var friendsbookFriendsController = currentView.GetComponent<FriendsbookFriendsController>();
        friendsbookFriendsController.SetFriends(character.friendsbookProfile.friends);
    }

}
