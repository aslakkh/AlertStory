using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookFriendsController : MonoBehaviour
{

    
    public int friendsPerPage;
    public GameObject listElementPrefab;
    public Button previousButton;
    public Button nextButton;
    public GameObject friendsListWrapperPrefab;
    private List<Character> friends;
    private GameObject friendsListWrapper;
    private FriendsbookMainController friendsbookMain;

    private int friendsListIndex;

    private void Start()
    {
        //sets up controller and displays friends
        friendsbookMain = GameObject.Find("FriendsbookMainView").GetComponent<FriendsbookMainController>();
        Debug.Assert(friendsbookMain != null, "Error: FriendsbookFriendsController could not find FriendsbookMainController.", this);
        friendsListIndex = 0;
        DisplayFriends();
        previousButton.interactable = false;
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
                nextButton.interactable = false;
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
        previousButton.interactable = true;
        DisplayFriends();
    }

    public void DecrementFriendsListIndex()
    {
        friendsListIndex -= friendsPerPage;
        nextButton.interactable = true;
        if(friendsListIndex == 0) { previousButton.interactable = false; }
        DisplayFriends();
    }

}
