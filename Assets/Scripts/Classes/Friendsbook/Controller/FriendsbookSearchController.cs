﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookSearchController : MonoBehaviour {

    public List<Character> characterList; //reference to ScriptableObject containing all in-game characters
    public InputField searchBar;
    public FriendsbookSearchView view;
    public Text text;

    private List<Character> friendsbookProfiles; //internal reference to all characters with friendsbookProfiles
    private List<Character> searchResult;
    private SoundManager soundManager;

    // Use this for initialization
	void Start () {
        characterList = GameObject.FindObjectOfType<GameManager>().characterList;
        if(characterList != null)
        {
            friendsbookProfiles = characterList.FindAll(c => c.hasFriendsbookProfile()); //filters out characters without friendsbookProfiles
        }
        
        searchResult = new List<Character>();
	    
	    soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

    public void SetFriendsbookProfiles(List<Character> list) //can be used to search from lists other than characterList
    {
        friendsbookProfiles = list;
    }

    public void SetCharacterList(List<Character> l)
    {
        characterList = l;
    }

    public List<Character> GetCharacterList()
    {
        return characterList;
    }

    public void Search(string term)
    {
        if (!string.IsNullOrEmpty(term))
        {
            searchResult = friendsbookProfiles.FindAll(c => c.fullName.ToLower().Contains(term.ToLower()));
            view.DisplaySearchResult(searchResult);
        }
        else
        {
            view.DisplaySearchResult(null);
        }
        
    }

    public void OnClick()
    {
        soundManager.sfxSource.Play();
        Search(text.text);
    }
	
    public void OnInputFieldValueChange(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            //hide
            view.DisplaySearchResult(null);
        }
    }
}
