﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//view for a character's friendsbook page
public class FriendsbookPersonView : MonoBehaviour {

    //UI elements
    public Text personName;
    public Image profilePicture;
    public Button addFriendButton;
    public Text friendRequestFeedback;
    public ScrollRect scrollRect;

    private Color positiveFeedbackColor = new Color(170, 193, 191, 255);
    private Color negativeFeedbackColor = new Color(190, 0, 0, 255);

    // Use this for initialization
    void Start () {
        if (friendRequestFeedback.gameObject.activeSelf) { friendRequestFeedback.gameObject.SetActive(false); } //hide feedback element
    }

    public void SetPersonName(Character character)
    {

        personName.text = character.fullName;
        if (character.friendsbookProfile.HasProfilePicture())
        {
            profilePicture.sprite = character.friendsbookProfile.profilePicture;
        }
    }

    //hide/display addfriendbutton
    public void SetFriendButtonActive(bool value)
    {
        addFriendButton.gameObject.SetActive(value);
    }

    //display feedback element
    public void DisplayFriendRequestFeedback(bool positive)
    {
        if (positive)
        {
            friendRequestFeedback.text = "Friend request accepted!";
            friendRequestFeedback.color = positiveFeedbackColor;
        }
        else
        {
            friendRequestFeedback.text = "Friend request denied";
            friendRequestFeedback.color = negativeFeedbackColor;
        }

        //display feedback for 7 seconds
        StartCoroutine(ShowFriendRequestFeedback(7f));

    }

    //activates feedback element s seconds
    public IEnumerator ShowFriendRequestFeedback(float s)
    {
        friendRequestFeedback.gameObject.SetActive(true);
        yield return new WaitForSeconds(s);
        friendRequestFeedback.gameObject.SetActive(false);
    }
}
