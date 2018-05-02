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
    // Use this for initialization
    void Start () {
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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNewGame()
    {
        //gm.dayCount = 0;
        gm.privateScore = 0;
        gm.score = 0;
        gm.playerCharacter.friendsbookProfile.friends.Clear();
        gm.turnCount = 0;
        gm.requirementDict.Clear();
        gm.eventsFired.Clear();
        // gm.eventsFired can return null if no events have passed (or the player is really lazy!)
        try
        {
            gm.eventsFired.Clear();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Eventsfired equal to null, moving on.");
        }
        GameObject.Destroy(gm.gameObject); // Destroys the gamemanager object.
        GameObject.Destroy(GameObject.Find("EventManager")); // Destroys the eventmanager object.

        // TODO: Access gamestate in gm and set it to investigator (default)

        //Changing scene to the settingScene after clearing all values.
        SceneManager.LoadScene("AppSettingsScene");
        //
    }

    public void ExitGame()
    {
        Debug.Log("Application is quitting!");
        // This call is ignored in the unity editor
        Application.Quit();
    }

}
