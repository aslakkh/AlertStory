using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using Settings;

public class NotificationBar : MonoBehaviour, IPointerClickHandler
{

    public Text stats;
    public Text objectives;
    public Text settings;
    //private Image image;
    private List<string> objectivesList;
    private RequirementDict settingsList;
    private Requirement requirement;

    public GameObject dropDown;
    private GameManager gameManager;

    void Awake()
    {
        //image = dropDown.GetComponent<Image>();
        dropDown.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stats = GameObject.Find("NotificationBar").transform.GetChild(0).GetComponent<Text>();
        objectives = dropDown.transform.Find("Objectives").GetComponent<Text>();
        GameManager.Instance.score = 10;
        GameManager.Instance.objectives = new List<string> { "Objective 1, Objective 2" };
        objectivesList = GameManager.Instance.objectives;
        settingsList = GameManager.Instance.requirements;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        stats.text = "Score: " + GameManager.Instance.score.ToString();
        objectives.text = "Objectives: " + GameManager.Instance.objectives.ToString();
        StringBuilder objectivesString = new StringBuilder();
        foreach (string objective in objectivesList)
        {
            objectivesString.Append(objective + ", " + "\n");
        }
        objectives.text = "Objectives: " + objectivesString.ToString();

        StringBuilder settingsString = new StringBuilder();


        foreach (KeyValuePair<Requirement, Setting> entry in settingsList.requirementDictionary)
        {
            settingsString.Append(entry.Value.requirementName + ", " + "\n");
        }
        settings.text = "Objectives: " + settingsString.ToString();

        if (dropDown.activeSelf)
        {
            dropDown.SetActive(false);
            dropDown.transform.SetAsFirstSibling();
        }
        else
        {
            dropDown.SetActive(true);
            dropDown.transform.SetAsLastSibling();
        }


    }

}
