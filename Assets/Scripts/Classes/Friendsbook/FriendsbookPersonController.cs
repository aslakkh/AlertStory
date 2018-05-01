using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Settings;

//Controls a character's friendsbook page
//Displays 1 subview (currentView) at a time
//interfaces with maincontroller
public class FriendsbookPersonController : MonoBehaviour {

    public FriendsbookPersonView viewComponent; //reference to view component script instance


    //prefabs for views to be added as child of this
    public GameObject aboutViewPrefab;
    public GameObject friendsViewPrefab;
    public GameObject hiddenViewPrefab;
    public GameObject postsViewPrefab;

    //settings determined what should be displayed in each subview. Set on initialization
    private Setting friendsSetting;
    private Setting informationSetting;
    private Setting postsSetting;

    private GameObject currentSubView; //current subview being rendered
    private Character character; //character this friendsbook page belongs to 
    private FriendsbookMainController friendsbookMainController;

	// Use this for initialization
	void Start () {

        //set person name
        if (character != null)
        {
            viewComponent.SetPersonName(character);
        }
        else
        {
            Debug.LogError("FriendsbookPersonController missing character", this);
        }

        bool friendsWithPlayer = friendsbookMainController.PlayerIsFriendsWith(character);
        SetSettings(friendsWithPlayer);

        if (friendsWithPlayer) //hide addfriend button
        {
            viewComponent.SetFriendButtonActive(false);
        }

        DisplayAboutView();
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
            //add friendship, update view with feedback + remove friendsbutton
            friendsbookMainController.AddFriendship(character);
            viewComponent.DisplayFriendRequestFeedback(positive:true);
            viewComponent.SetFriendButtonActive(false);
        }
        else
        {
            //update view with negative feedback
            viewComponent.DisplayFriendRequestFeedback(positive:false);
        }
        
        
    }


    //display aboutview as subview
    public void DisplayAboutView()
    {
        if(currentSubView != null) //only one subview at a time
        {
            GameObject.Destroy(currentSubView);
        }

        //subview content is decided by informationSetting
        if(informationSetting == Setting.Public)
        {
            currentSubView = Instantiate(aboutViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.transform.localScale = Vector3.one;
            //init about view with character information
            currentSubView.GetComponent<FriendsbookAboutView>().SetInformation(character);
        }
        else if(informationSetting == Setting.Private)
        {
            currentSubView = Instantiate(hiddenViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person has their personal information set to private.");
        }
        else
        {
            currentSubView = Instantiate(hiddenViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person only shares their personal information with friends.");
        }
        
    }

    //display posts view as subview
    public void DisplayPostsView()
    {
        if (currentSubView != null) //only one subview at a time
        {
            GameObject.Destroy(currentSubView);
        }
        if(postsSetting == Setting.Public)
        {
            if(character.friendsbookProfile.HasPosts())
            {
                currentSubView = Instantiate(postsViewPrefab);
                currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
                var postsview = currentSubView.GetComponent<FriendsbookPostsView>();
                postsview.DisplayPosts(character.friendsbookProfile.posts.list);
            }
            else
            {
                currentSubView = Instantiate(hiddenViewPrefab);
                currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
                currentSubView.GetComponent<FriendsbookHiddenView>().SetText("No posts.");
            }
            
        }
        else if(postsSetting == Setting.Private)
        {
            currentSubView = Instantiate(hiddenViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person has posts set to private.");
        }
        else //postsSetting is friends
        {
            currentSubView = Instantiate(hiddenViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person only displays their posts to other friends.");
        }
        
    }

    //display friendsview as subview
    public void DisplayFriendsView()
    {
        if (currentSubView != null) //only one subview at a time
        {
            GameObject.Destroy(currentSubView);
        }
        if (friendsSetting == Setting.Public)
        {
            if (character.friendsbookProfile.HasFriends())
            {
                currentSubView = Instantiate(friendsViewPrefab);
                currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
                var friendsbookFriendsController = currentSubView.GetComponent<FriendsbookFriendsController>();
                friendsbookFriendsController.SetFriends(character.friendsbookProfile.friends);
            }
            else
            {
                currentSubView = Instantiate(hiddenViewPrefab);
                currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
                currentSubView.GetComponent<FriendsbookHiddenView>().SetText("No friends.");
            }

        }
        else if(friendsSetting == Setting.Private)
        {
            currentSubView = Instantiate(hiddenViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person has friends set to private.");
        }
        else if(friendsSetting == Setting.Friends)
        {
            currentSubView = Instantiate(hiddenViewPrefab);
            currentSubView.transform.SetParent(viewComponent.scrollRect.content, false);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person only displays their friendslist to other friends.");
        }

        
    }

}
