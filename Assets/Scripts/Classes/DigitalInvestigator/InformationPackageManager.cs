using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPackageManager : MonoBehaviour {

	private GameManager gameManager;
	private Dictionary<int, List<Objective>> objectives;
	private Dictionary<int, List<string>> informationPackage;
	public int dayCount = 0;

	void Start() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		objectives = gameManager.objectives; // = gm.objectivesDict <int, List<Objective>>
        informationPackage = gameManager.informationDict; // gm._informationDict <int, List<string>>
        dayCount = gameManager.GetDayCount();
	}

	// Validates the information gathered by the user after the user has delivered the information
	// or time is up

	// Sorry in advance ...
	public void ValidateInformationGathered() {
		dayCount = gameManager.GetDayCount();
        if (informationPackage != null)
        {
            foreach (KeyValuePair<int, List<string>> info in informationPackage)
            {
                if (info.Key == dayCount)
                {
                    for (int i =0 ; i < info.Value.Count; i += 2)
                    {
                        string temp = info.Value[i];
                        foreach (KeyValuePair<int, List<Objective>> obj in objectives)
                        {
                            if (obj.Key == dayCount)
                            {
                                foreach (Objective o in obj.Value)
                                {
                                    foreach (string task in o.tasks)
                                    {
                                        if (task.Equals(temp) || temp.Contains(task))
                                        {
                                            gameManager.AddToScore(20);
                                        }
                                        else
                                        {
                                            gameManager.AddToScore(-5);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            gameManager.AddToScore(-25);
        }
		
	}

}
