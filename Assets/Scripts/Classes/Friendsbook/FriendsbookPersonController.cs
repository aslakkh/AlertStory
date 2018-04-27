using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Settings;

public class FriendsbookPersonController : MonoBehaviour {

    public Text personName;
    public Image profilePicture;
    public Button addFriendButton;
    public Text friendRequestFeedback;
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
    private FriendsbookMainController friendsbookMainController;

	// Use this for initialization
	void Start () {
        friendRequestFeedback.gameObject.SetActive(false);
        SetPersonName();
        bool friendsWithPlayer = friendsbookMainController.PlayerIsFriendsWith(character);
        SetSettings(friendsWithPlayer);
        if (friendsWithPlayer)
        {
            addFriendButton.gameObject.SetActive(false);
        }
        DisplayAboutView();
	}
	

    public void SetPersonName()
    {
        if(character != null)
        {
            personName.text = character.fullName;
            if (character.friendsbookProfile.HasProfilePicture())
            {
                profilePicture.sprite = character.friendsbookProfile.profilePicture;
            }

            
        }
        else
        {
            Debug.LogError("FriendsbookPersonController missing character", this);
        }
        
    }

    //public void SetImage()
    //{

    //}

    public void SetSettings(bool friendsWithPlayer)
    {
        friendsSetting = character.friendsbookProfile.friendsSetting;
        informationSetting = character.friendsbookProfile.informationSetting;
        postsSetting = character.friendsbookProfile.postsSetting;
        //"FRIEND" setting behaves as public if player is friend with the character
        if(friendsSetting == Setting.Friends && friendsWithPlayer) { friendsSetting = Setting.Public; }
        if (informationSetting == Setting.Friends && friendsWithPlayer) { informationSetting = Setting.Public; }
        if (postsSetting == Setting.Friends && friendsWithPlayer) { postsSetting = Setting.Public; }

    }


    public void SetCharacter(Character c) { character = c; }

    public void SetFriendsbookMainController(FriendsbookMainController c) { friendsbookMainController = c; }

    //checks if friend request is accepted. Displays feedback
    public void OnAddFriendButtonClick()
    {
        if (character.friendsbookProfile.acceptsFriendRequest)
        {
            friendsbookMainController.AddFriendship(character);
            friendRequestFeedback.text = "Friend request accepted!";
            friendRequestFeedback.color = Color.green;
            addFriendButton.gameObject.SetActive(false);
        }
        else
        {
            friendRequestFeedback.text = "Friend request denied";
            friendRequestFeedback.color = Color.red;
        }
        StartCoroutine(ShowFriendRequestFeedback());
        
    }

    public IEnumerator ShowFriendRequestFeedback()
    {
        friendRequestFeedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        friendRequestFeedback.gameObject.SetActive(false);
    }

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
            if(character.friendsbookProfile.HasPosts())
            {
                currentView = Instantiate(postsViewPrefab);
                currentView.transform.SetParent(scrollRect.content, false);
                var postsController = currentView.GetComponent<FriendsbookPostsController>();
                postsController.SetPosts(character.friendsbookProfile.posts.list);
            }
            else
            {
                currentView = Instantiate(hiddenViewPrefab);
                currentView.transform.SetParent(scrollRect.content, false);
                currentView.GetComponentInChildren<Text>().text = "No posts.";
            }
            
        }
        else if(postsSetting == Setting.Private)
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person has posts set to private.";
        }
        else //postsSetting is friends
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person only displays their posts to other friends.";
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
            if (character.friendsbookProfile.HasFriends())
            {
                currentView = Instantiate(friendsViewPrefab);
                currentView.transform.SetParent(scrollRect.content, false);
                var friendsbookFriendsController = currentView.GetComponent<FriendsbookFriendsController>();
                friendsbookFriendsController.SetFriends(character.friendsbookProfile.friends);
            }
            else
            {
                currentView = Instantiate(hiddenViewPrefab);
                currentView.transform.SetParent(scrollRect.content, false);
                currentView.GetComponentInChildren<Text>().text = "No friends.";
            }

        }
        else if(friendsSetting == Setting.Private)
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person has friends set to private.";
        }
        else if(friendsSetting == Setting.Friends)
        {
            currentView = Instantiate(hiddenViewPrefab);
            currentView.transform.SetParent(scrollRect.content, false);
            currentView.GetComponentInChildren<Text>().text = "This person only displays their friendslist to other friends.";
        }

        
    }

}
