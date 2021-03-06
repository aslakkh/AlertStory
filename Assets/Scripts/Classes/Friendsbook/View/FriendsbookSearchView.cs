﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookSearchView : MonoBehaviour {

    public ScrollRect scrollRectPrefab;
    public GameObject noResultPrefab;
    public GameObject searchResultElement;
    public Color color1;
    public Color color2;
    public FriendsbookMainController controller;

    private ScrollRect scrollRect;

    private List<GameObject> searchResults;

    private void Start()
    {
        searchResults = new List<GameObject>();
    }

    public void DisplaySearchResult(List<Character> characters)
    {

        foreach(GameObject e in searchResults)
        {
            GameObject.Destroy(e);
        }
        if(characters != null && characters.Count > 0)
        {
            SetVisible();
            int counter = 0;
            foreach (Character c in characters)
            {
                if (counter >= 10)
                {
                    break;
                }
                string name = c.fullName;
                GameObject element = Instantiate(searchResultElement);
                element.transform.SetParent(scrollRect.content, false); //recommended way of setting parent of UI element
                element.GetComponentInChildren<Text>().text = name;
                if (c.friendsbookProfile.HasProfilePicture())
                {
                    element.transform.GetChild(0).GetComponent<Image>().sprite = c.friendsbookProfile.profilePicture;
                }
                element.GetComponent<Image>().color = (counter % 2 == 0) ? color1 : color2; //color alternates
                Button b = element.AddComponent<Button>();
                b.onClick.AddListener(delegate () { OnSearchElementClick(c); });
                searchResults.Add(element);
                counter++;
            }
        }
        else
        {
            if(characters == null)
            {
                Hide();
            }
            else
            {
                SetVisible();
                GameObject element = Instantiate(noResultPrefab);
                element.transform.SetParent(scrollRect.content, false); //recommended way of setting parent of UI element
                element.GetComponentInChildren<Text>().text = "No results...";
                searchResults.Add(element);
            }
            
        }
        
    }

    public void OnSearchElementClick(Character c)
    {
        Hide();
        if(c.fullName == controller.GetGameManager().playerCharacter.fullName)
        {
            controller.EnterPlayerCharacterProfile();
        }
        else
        {
            controller.EnterFriendsbookProfile(c);
        }
    }

    public void SetVisible()
    {
        if(scrollRect == null) { scrollRect = Instantiate(scrollRectPrefab, transform.parent); } 
    }

    public void Hide()
    {
        if(scrollRect != null)
        {
            GameObject.Destroy(scrollRect.gameObject);
        }
        
    }
}
