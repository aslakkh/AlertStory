using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookSearchView : MonoBehaviour {

    public ScrollRect scrollRectPrefab;
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
        if(characters != null)
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
                element.GetComponent<Image>().color = (counter % 2 == 0) ? color1 : color2; //color alternates
                Button b = element.AddComponent<Button>();
                b.onClick.AddListener(delegate () { OnSearchElementClick(c); });
                searchResults.Add(element);
                counter++;
            }
        }
        else
        {
            Hide();
        }
        
    }

    public void OnSearchElementClick(Character c)
    {
        Hide();
        controller.EnterFriendsbookProfile(c);
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
