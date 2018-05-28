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
    private SoundManager soundManager;

    // Use this for initialization
	void Start () {
	    soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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
	

    public void SetSettings(bool friendsWithPlayer)
    {
        friendsSetting = character.friendsbookProfile.friendsSetting;
        informationSetting = character.friendsbookProfile.informationSetting;
        postsSetting = character.friendsbookProfile.postsSetting;

        //"FRIEND" setting behaves as public if player is friend with the character
        if (friendsSetting == Setting.Friends && friendsWithPlayer) { friendsSetting = Setting.Public; }
        if (informationSetting == Setting.Friends && friendsWithPlayer) { informationSetting = Setting.Public; }
        if (postsSetting == Setting.Friends && friendsWithPlayer) { postsSetting = Setting.Public; }


    }


    public void SetCharacter(Character c)
    {
        if (c.hasFriendsbookProfile())
        {
            character = c;
        }
        else
        {
            Debug.LogError("Tried setting character of friendsbookPersonController to character without friendsbook profile");
        }
       
    }

    public void SetFriendsbookMainController(FriendsbookMainController c) { friendsbookMainController = c; }

    //checks if friend request is accepted. Displays feedback
    public void OnAddFriendButtonClick()
    {
        soundManager.sfxSource.Play();
        if (character.friendsbookProfile.acceptsFriendRequest)
        {
            //add friendship, update view with feedback + remove friendsbutton
            friendsbookMainController.AddFriendship(character);
            viewComponent.DisplayFriendRequestFeedback(positive:true);
            viewComponent.SetFriendButtonActive(false);

            //update settings to reflect friendship status change
            SetSettings(friendsWithPlayer: true);
            DisplayAboutView();

        }
        else
        {
            //update view with negative feedback
            viewComponent.DisplayFriendRequestFeedback(positive:false);
        }
        
        
    }

    private void DestroyExistingSubView()
    {
        if (currentSubView != null) //only one subview at a time
        {
            GameObject.Destroy(currentSubView);
        }
    }

    //instantiate prefab and set as current sub view
    private void SetCurrentSubView(GameObject prefab)
    {
        currentSubView = Instantiate(prefab);
        currentSubView.transform.SetParent(viewComponent.scrollRect.content, false); //recommended way of setting parent of UI object
        currentSubView.transform.localScale = Vector3.one;
    }

    //display aboutview as subview
    public void DisplayAboutView()
    {
        DestroyExistingSubView(); //only one subview at a time

        soundManager.sfxSource.Play();
        
        //subview content is decided by informationSetting
        if(informationSetting == Setting.Public)
        {
            SetCurrentSubView(aboutViewPrefab);
            //init about view with character information
            currentSubView.GetComponent<FriendsbookAboutView>().SetInformation(character);
        }
        else if(informationSetting == Setting.Private)
        {
            SetCurrentSubView(hiddenViewPrefab);
            if(currentSubView.GetComponent<FriendsbookHiddenView>() != null)
            {
                currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person has their personal information set to private.");
            }
           
        }
        else
        {
            SetCurrentSubView(hiddenViewPrefab);
            if (currentSubView.GetComponent<FriendsbookHiddenView>() != null)
            {
                currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person only shares their personal information with friends.");
            }
        }
        
    }

    //display posts view as subview
    public void DisplayPostsView()
    {
        DestroyExistingSubView(); //only one subview at a time
        soundManager.sfxSource.Play();
        if (postsSetting == Setting.Public)
        {
            if(character.friendsbookProfile.HasPosts())
            {
                SetCurrentSubView(postsViewPrefab);
                var postsview = currentSubView.GetComponent<FriendsbookPostsView>();
                postsview.DisplayPosts(character.friendsbookProfile.posts.list);
            }
            else
            {
                SetCurrentSubView(hiddenViewPrefab);
                currentSubView.GetComponent<FriendsbookHiddenView>().SetText("No posts.");
            }
            
        }
        else if(postsSetting == Setting.Private)
        {
            SetCurrentSubView(hiddenViewPrefab);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person has posts set to private.");
        }
        else //postsSetting is friends
        {
            SetCurrentSubView(hiddenViewPrefab);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person only displays their posts to other friends.");
        }
        
    }

    //display friendsview as subview
    public void DisplayFriendsView()
    {
        DestroyExistingSubView(); //only one subview at a time
        
        soundManager.sfxSource.Play();
        if (friendsSetting == Setting.Public)
        {
            if (character.friendsbookProfile.HasFriends())
            {
                SetCurrentSubView(friendsViewPrefab);
                var friendsbookFriendsController = currentSubView.GetComponent<FriendsbookFriendsController>();
                friendsbookFriendsController.SetFriends(character.friendsbookProfile.friends);
            }
            else
            {
                SetCurrentSubView(hiddenViewPrefab);
                currentSubView.GetComponent<FriendsbookHiddenView>().SetText("No friends.");
            }

        }
        else if(friendsSetting == Setting.Private)
        {
            SetCurrentSubView(hiddenViewPrefab);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person has friends set to private.");
        }
        else if(friendsSetting == Setting.Friends)
        {
            SetCurrentSubView(hiddenViewPrefab);
            currentSubView.GetComponent<FriendsbookHiddenView>().SetText("This person only displays their friendslist to other friends.");
        }
    }

    //getters for controller settings
    public Setting GetFriendsSetting() { return friendsSetting; }
    public Setting GetInfoSetting() { return informationSetting; }
    public Setting GetPostsSetting() { return postsSetting; }

    public GameObject GetCurrentSubView() { return currentSubView; }

    public Character GetCharacter() { return character; }
}
