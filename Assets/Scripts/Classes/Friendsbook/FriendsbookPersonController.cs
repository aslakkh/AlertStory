using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Settings;

public class FriendsbookPersonController : MonoBehaviour {

    public Text personName;
    public GameObject aboutViewPrefab;
    public GameObject friendsViewPrefab;
    public GameObject hiddenViewPrefab;
    public GameObject postsViewPrefab;
    public ScrollRect scrollRect;

    private Setting friendsSetting;
    private Setting informationSetting;
    private Setting postsSetting;
    private GameObject currentView;
    private Character character;
    private Character playerCharacter;

	// Use this for initialization
	void Start () {
        SetPersonName();
        SetSettings();
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

    public void SetSettings()
    {
        friendsSetting = character.friendsbookProfile.friendsSetting;
        informationSetting = character.friendsbookProfile.informationSetting;
        postsSetting = character.friendsbookProfile.postsSetting;
    }


    public void SetCharacter(Character c) { character = c; }

    public void DisplayAboutView()
    {
        
        if(currentView != null)
        {
            GameObject.Destroy(currentView);
        }
        if(informationSetting == Setting.Public)
        {
            currentView = Instantiate(aboutViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.transform.localScale = Vector3.one;
            currentView.GetComponent<FriendsbookAboutController>().SetInformation(character);
        }
        else if(informationSetting == Setting.Private)
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person has their personal information set to private.";
        }
        else
        {
            //check if characters are friends
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person only shares their personal information with friends.";
        }
        
    }

    public void DisplayPostsView()
    {
        if (currentView != null)
        {
            GameObject.Destroy(currentView);
        }
        if(postsSetting == Setting.Public)
        {
            currentView = Instantiate(postsViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            var postsController = currentView.GetComponent<FriendsbookPostsController>();
            postsController.SetPosts(character.friendsbookProfile.posts.list);
        }
        else if(postsSetting == Setting.Private)
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person has posts set to private.";
        }
        else //postsSetting is friends
        {
            //check if player is friends with character
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person only displays their friendslist to other friends.";
        }
        
    }

    public void DisplayFriendsView()
    {
        if (currentView != null)
        {
            GameObject.Destroy(currentView);
        }
        if (friendsSetting == Setting.Public)
        {
            currentView = Instantiate(friendsViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            var friendsbookFriendsController = currentView.GetComponent<FriendsbookFriendsController>();
            friendsbookFriendsController.SetFriends(character.friendsbookProfile.friends);
        }
        else if(friendsSetting == Setting.Private)
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person has friends set to private.";
        }
        else if(friendsSetting == Setting.Friends)
        {
            //should check if player is friends with character
        }

        
    }

}
