using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class InformationPackageManager : MonoBehaviour {

	private GameManager gameManager;
	private Dictionary<int, List<Objective>> objectives;
	private List<string> informationPackage;
	public int dayCount = 0;

	void Start() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		objectives = gameManager.objectives; // = gm.objectivesDict <int, List<Objective>>
        informationPackage = gameManager.informationPackage; 
        dayCount = gameManager.GetDayCount();
	}

	// Validates the information gathered by the user after the user has delivered the information
	// or time is up

	// Sorry in advance ...
	public void ValidateInformationGathered() {
		dayCount = gameManager.GetDayCount();
        int minusScore = 0;
        int plusScore = 0;
        int informationCount = informationPackage.Count / 2;
        if (informationPackage != null)
        {
            foreach (KeyValuePair<int, List<Objective>> obj in objectives)
            {
                if (obj.Key == dayCount)
                {
                    foreach (Objective o in obj.Value)
                    {
                        for (int i = 0; i < informationPackage.Count; i += 2)
                        {
                            bool taskmatch = false;
                            string temp = informationPackage[i + 1];
                            for(int j = 0; j < o.tasks.Count;j++)
                            {
                                if (taskmatch == false)
                                {
                                    // DEBUGGING FOR SPECIAL ANNOYING STRING THAT WONT MATCH (mother post task on day 2)
                                    string x = "Im so glad to be 50 years old. I know i'm getting old, but you know what keeps me up and going? My children. They're the best thing that ever happened to me, and I'm so proud of them. Thanks for all birthday wishes!";
                                    if (o.tasks[j].Equals(x))
                                    {
                                        Debug.Log(temp.Length);
                                        string newString = temp.Replace("\n","").Replace("\r","");
                                        Debug.Log(o.tasks[j].Equals(newString));
                                        Debug.Log(o.tasks[j].Length);
                                        Debug.Log("tasks compare to x");
                                    }
                                    if (temp.Equals(x))
                                    {
                                        Debug.Log("temp compares to x");
                                    }
                                    if (o.tasks[j].Equals(temp) || temp.Contains(o.tasks[j]))
                                    {
                                        taskmatch = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }

                            }
                            if (taskmatch == false)
                            {
                                Debug.Log("This one is not a match: " + temp);
                                minusScore -= 10;
                            }
                            else
                            {
                                plusScore += 50;
                            }
                        }
                        if (informationCount < o.tasks.Count)
                        {
                            int difference = (o.tasks.Count - informationCount) * 10;
                            minusScore -= difference;
                        }
                       
                    }
                }
            }
            gameManager.AddToScore(plusScore);
            gameManager.AddToScore(minusScore);
        }
        else
        {
            Debug.Log("Information package is null, gving -25 penalty to score..");
            gameManager.AddToScore(-25);
        }
		
	}

}
