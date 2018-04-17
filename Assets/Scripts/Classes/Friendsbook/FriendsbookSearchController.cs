using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookSearchController : MonoBehaviour {

    /// TODO
    /// - display/hide search results
    /// - handle clicks on search element
    /// - optimization
    /// 

    public CharacterList characterList; //reference to ScriptableObject containing all in-game characters
    public InputField searchBar;
    public FriendsbookSearchView view;

    private List<Character> friendsbookProfiles; //internal reference to all characters with friendsbookProfiles

	// Use this for initialization
	void Start () {
        friendsbookProfiles = characterList.list.FindAll(c => c.hasFriendsbookProfile()); //filters out characters without friendsbookProfiles
	}

    public void Search(string term)
    {
        if (!string.IsNullOrEmpty(term))
        {
            friendsbookProfiles = characterList.list.FindAll(c => c.fullName.ToLower().Contains(term.ToLower()));
            view.DisplaySearchResult(friendsbookProfiles);
        }
        
    }
	
}
