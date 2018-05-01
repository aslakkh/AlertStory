using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controller for friendsbook friends view (subview of character profile page)
public class FriendsbookFriendsController : MonoBehaviour
{
    public FriendsbookFriendsView viewComponent; //reference to view component script instance
    public int friendsPerPage; //friends to view per page
    private int friendsListIndex; //current index in friendsList

    public GameObject friendsListWrapperPrefab;
    public GameObject listElementPrefab;
    private List<Character> friends;
    private GameObject friendsListWrapper;
    private FriendsbookMainController friendsbookMain;

    private void Start()
    {
        //sets up controller and displays friends
        friendsListIndex = 0;
        friendsbookMain = GameObject.Find("FriendsbookMainView").GetComponent<FriendsbookMainController>();
        Debug.Assert(friendsbookMain != null, "Error: FriendsbookFriendsController could not find FriendsbookMainController.", this);
        DisplayFriends();
        
    }

    public void SetFriends(List<Character> friends)
    {
        this.friends = friends;
    }

    public void DisplayFriends()
    {
        if (friendsListWrapper)
        {
            GameObject.Destroy(friendsListWrapper);
        }
        friendsListWrapper = Instantiate(friendsListWrapperPrefab);
        friendsListWrapper.transform.SetParent(transform, false);
        for(int i = friendsListIndex; i < friendsListIndex+friendsPerPage; i++)
        {
            if(i >= friends.Count)
            {
                viewComponent.SetNextButtonInteractable(false);
                break;
            }

            Character c = friends[i];
            GameObject t = Instantiate(listElementPrefab);
            t.transform.SetParent(friendsListWrapper.transform, false);
            t.GetComponentInChildren<Text>().text = c.fullName;
            t.GetComponent<Button>().onClick.AddListener(delegate () { OnListElementClick(c); });
        }
    }

    public void OnListElementClick(Character c)
    {
        friendsbookMain.EnterFriendsbookProfile(c);
    }

    public void IncrementFriendsListIndex()
    {
        friendsListIndex += friendsPerPage;
        viewComponent.SetPreviousButtonInteractable(true);
        DisplayFriends();
    }

    public void DecrementFriendsListIndex()
    {
        friendsListIndex -= friendsPerPage;
        viewComponent.SetNextButtonInteractable(true);
        if(friendsListIndex == 0) { viewComponent.SetPreviousButtonInteractable(false); }
        DisplayFriends();
    }

}
