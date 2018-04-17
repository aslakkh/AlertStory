using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookSearchView : MonoBehaviour {

    public ScrollRect scrollRect;
    public GameObject searchResultElement;

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
        int counter = 0;
        foreach(Character c in characters)
        {
            if(counter >= 10)
            {
                break;
            }
            string name = c.fullName;
            GameObject element = Instantiate(searchResultElement, scrollRect.content);
            element.GetComponentInChildren<Text>().text = name;
            searchResults.Add(element);
            counter++;
        }
    }
}
