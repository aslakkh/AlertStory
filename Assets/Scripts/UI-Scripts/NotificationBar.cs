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
    private List<string> objectivesList;
    private StringSettingDictionary settingsList;
    private Requirement requirement;

    public GameObject scrollView;
    public GameObject dropDown;
    private GameManager gameManager;

    void Start()
    {
        settingsList = GameManager.Instance.requirements;
        GameManager.Instance.objectives = new List<string> { "Objective 1, Objective 2" };
        objectivesList = GameManager.Instance.objectives;

    }

    void Awake()
    {
        scrollView.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stats = GameObject.Find("NotificationBar").transform.GetChild(0).GetComponent<Text>();
        objectives = dropDown.transform.Find("Objectives").GetComponent<Text>();

    }

    void Update()
    {
        stats.text = "Score: " + gameManager.score.ToString();
    }
    

    //Opens dropdown
    public void OnPointerClick(PointerEventData eventData)
    {
        //Sets score string
        stats.text = "Score: " + gameManager.score;
        //Sets objectives string
        StringBuilder objectivesString = new StringBuilder();
        foreach (string objective in objectivesList)
        {

            objectivesString.Append(objective + ", " + "\n");
        }
        objectives.text = "Objectives: " + "\n" + objectivesString.ToString();

        //Sets settings string 
        settings.text = "Settings: " + "\n";
        StringBuilder settingsString = new StringBuilder();
        foreach (KeyValuePair<string, Setting> item in settingsList)
        {
            settingsString.Append(item.Key + ": " + item.Value + ", " + "\n");
        }
        settings.text = "Settings: " + "\n" + settingsString.ToString();
        
        if (scrollView.activeSelf)
        {
            scrollView.SetActive(false);
            scrollView.transform.SetAsFirstSibling();
        }
        else
        {
            scrollView.SetActive(true);
            scrollView.transform.SetAsLastSibling();
        }


    }

}
