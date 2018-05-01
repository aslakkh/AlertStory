using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookSearchController : MonoBehaviour {

    /// TODO
    /// - optimization
    /// 

    public CharacterList characterList; //reference to ScriptableObject containing all in-game characters
    public InputField searchBar;
    public FriendsbookSearchView view;
    public Text text;

    private List<Character> friendsbookProfiles; //internal reference to all characters with friendsbookProfiles
    private List<Character> searchResult;

	// Use this for initialization
	void Start () {
        if(characterList != null)
        {
            friendsbookProfiles = characterList.list.FindAll(c => c.hasFriendsbookProfile()); //filters out characters without friendsbookProfiles
        }
        
        searchResult = new List<Character>();
	}

    public void SetFriendsbookProfiles(List<Character> list) //can be used to search from lists other than characterList
    {
        friendsbookProfiles = list;
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
        //debug.log character clicked
        Search(text.text);
    }
	
}
