using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookFriendsController : MonoBehaviour
{

    
    public int friendsPerPage;
    public Text listElementPrefab;
    public Button previousButton;
    public Button nextButton;
    public GameObject friendsListWrapperPrefab;
    private List<Character> friends;
    private GameObject friendsListWrapper;

    private int friendsListIndex;

    private void Start()
    {
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
            Text t = Instantiate(listElementPrefab);
            t.transform.SetParent(friendsListWrapper.transform, false);
            t.text = c.fullName;
        }
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
