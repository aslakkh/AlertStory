using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class EndGameController : MonoBehaviour {

    private GameManager gm;
    private int previousHighScore;
    private int currentScore;
    private Text SaveStatus;
    public GameObject prefabPointer;

    // Use this for initialization
    void Start () {
        ControllScore();
        RetrieveEventHistory();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void ControllScore()
    {
        GameObject gameObj = GameObject.Find("GameManager");
        gm = gameObj.GetComponent<GameManager>();
        previousHighScore = PlayerPrefs.GetInt("highscore");
        currentScore = gm.score + gm.privateScore;
        SaveStatus = GameObject.Find("SaveStatusText").GetComponent<Text>();
        if (currentScore > previousHighScore)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
            SaveStatus = GameObject.Find("SaveStatusText").GetComponent<Text>();
            SaveStatus.text = "Your new highscore have been saved!";
        }
        else
        {
            SaveStatus.text = "You were " + (previousHighScore - currentScore) + " points in score away from beating the highscore";
        }
        Text score = GameObject.Find("YourScoreText").GetComponent<Text>();
        score.text = score.text + currentScore;
    }

    public void RetrieveEventHistory()
    {
        GameObject panel = GameObject.Find("EndgameScrollviewContentPanel");

        foreach (KeyValuePair<StoryEvent,Choice> entry in gm.eventsFired){
            GameObject temp = Instantiate(prefabPointer) as GameObject;
            if (panel != null)
            {
                temp.transform.SetParent(panel.transform, false);
                Transform transformEvent = temp.transform.GetChild(0);
                Transform transformChoice = temp.transform.GetChild(1);
                transformEvent.GetComponent<Text>().text = entry.Key.text;
                transformChoice.GetComponent<Text>().text = entry.Value.choiceDescription;
            }
        }
    }

    public void StartNewGame()
    {
        gm.playerCharacter.friendsbookProfile.friends.Clear(); // its a file so better clear it, since its static. 
        GameObject.Destroy(gm.gameObject); // Destroys the gamemanager object.
        GameObject.Destroy(GameObject.Find("EventManager")); // Destroys the eventmanager object.


        //Changing scene to the settingScene after clearing all values.
        SceneManager.LoadScene("AppSettingsScene");
    }

    public void ExitGame()
    {
        Debug.Log("Application is quitting!");
        // This call is ignored in the unity editor
        Application.Quit();
    }

}
