using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class manages which app is currently being viewed, and allows for navigation to/from homepage
public class AppManager : MonoBehaviour {

    public GameObject friendsbookPrefab; //prefab for friendsbook app
    public GameObject guideAppPrefab;
    public Transform appSpawnPoint; //where should apps be instantiated?
    public GameObject dropDownScrollView; //reference to scrollview 

    private GameObject currentApp;
    private GameManager gameManager; //reference to gamemanager


    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    public void OpenFriendsbook()
    {
        CloseCurrentApp(); //only allow one app open. Force close any existing app
        currentApp = Instantiate(friendsbookPrefab);
        currentApp.transform.SetParent(appSpawnPoint, false);
        currentApp.name = "FriendsbookMainView";
    }

    //open guide app
    public void OpenGuide()
    {
        CloseCurrentApp();
        currentApp = Instantiate(guideAppPrefab);
        currentApp.transform.SetParent(appSpawnPoint, false);
        currentApp.name = "GuideApp";

        //guide app should pause the game
        gameManager.SetPaused();
    }

    public void CloseCurrentApp()
    {
        //close dropdown scroll view if it is active
        if (dropDownScrollView.activeSelf)
        {
            dropDownScrollView.SetActive(false);
        }
        else
        {
            //close any displaying app
            if (currentApp != null)
            {
                if (currentApp.name == "GuideApp") //unpause
                {
                    gameManager.SetPaused();
                }
                GameObject.Destroy(currentApp);
            }
        }
        
    }
}
