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


    // retrieves the highest score, compare it, and store it if its a new highscore.
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

    // retrieves the event history from gm.eventsfired. Only takes the multiple choice events.
    public void RetrieveEventHistory()
    {
        GameObject panel = GameObject.Find("EndgameScrollViewContent");
        foreach (KeyValuePair<StoryEvent,Choice> entry in gm.eventsFired){
            if (panel != null)
            {
                // only retrieve events which are multiple choice
                if (entry.Key.IsMultipleChoice())
                {
                    GameObject temp = Instantiate(prefabPointer) as GameObject;
                    temp.transform.SetParent(panel.transform, false);
                    Transform transformEvent = temp.transform.GetChild(0);
                    Transform transformChoice = temp.transform.GetChild(1);
                    if (String.IsNullOrEmpty(entry.Key.text))
                    {
                        transformEvent.GetComponent<Text>().text = "Missing event text. Add it!";
                    }
                    else
                    {
                        transformEvent.GetComponent<Text>().text = entry.Key.text;
                    }
                    transformChoice.GetComponent<Text>().text = entry.Value.choiceDescription;

                    int tempval = 0;
                    foreach (Score score in entry.Value.scores)
                    {
                        if (gm.requirementDict.ContainsKey(score.requirementName) && gm.requirementDict[score.requirementName] == score.setting)
                        {
                            tempval += score.value;
                        }
                        if (string.IsNullOrEmpty(score.requirementName))
                        {
                            if (score.value != 0)
                            {
                                tempval += score.value;
                            }
                        }
                    }
                    if (tempval > 0)
                    {
                        temp.GetComponent<Image>().color = Color.green;
                    }
                    if (tempval < 0)
                    {
                        temp.GetComponent<Image>().color = Color.red;
                    }
                }

            }
        }
    }

    public void StartNewGame()
    {
        gm.playerCharacter.friendsbookProfile.friends.Clear(); // its a file so better clear it, since its static. 
        gm.ResetDayCount();
        GameObject.Destroy(gm.gameObject); // Destroys the gamemanager object.
        GameObject.Destroy(GameObject.Find("EventManager")); // Destroys the eventmanager object.


        //Changing scene to the settingScene after clearing all values.
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadAppSettingsScene();
    }

    public void ExitGame()
    {
        Debug.Log("Application is quitting!");
        // This call is ignored in the unity editor
        Application.Quit();
    }

}
