using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//component for updating view of friendsbookfriends (subview of friendsbookpersonview)
public class FriendsbookFriendsView : MonoBehaviour {


    public Button previousButton;
    public Button nextButton;
    

    // Use this for initialization
    void Start () {
        SetPreviousButtonInteractable(false);
    }

    public void SetNextButtonInteractable(bool value)
    {
        nextButton.interactable = value;
    } 

    public void SetPreviousButtonInteractable(bool value)
    {
        previousButton.interactable = value;
    }
	
}
