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
    public Text informationPackageText;
    private Dictionary<int, Objective> objectivesList;
    private StringSettingDictionary settingsList;
    private Requirement requirement;
    private List<string> informationPackage;

    public GameObject scrollView;
    public GameObject dropDown;
    private GameManager gameManager;

    void Start()
    {
        settingsList = GameManager.Instance.requirements;
        objectivesList = GameManager.Instance.objectives;
        informationPackage = GameManager.Instance.informationPackage;
    }

    void Awake()
    {
        scrollView.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stats = GameObject.Find("NotificationBar").transform.GetChild(0).GetComponent<Text>();
        objectives = dropDown.transform.Find("Objectives").GetComponent<Text>();
        informationPackageText = dropDown.transform.Find("Digital Investigator List").GetChild(0).GetComponent<Text>();
        informationPackageText.text = "Information package" + "\n" + "This information will be submitted. Click on an item to remove it." + "\n\n";
    }

    //Opens dropdown
    public void OnPointerClick(PointerEventData eventData)
    {
        //Sets score string
        stats.text = "Score: " + GameManager.Instance.score.ToString();

        //Sets objectives string
        StringBuilder objectivesString = new StringBuilder();
        foreach (KeyValuePair<int, Objective> objective in objectivesList)
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


        informationPackage = GameManager.Instance.informationPackage;
        StringBuilder informationPackageString = new StringBuilder();
        foreach(string element in informationPackage)
        {
            informationPackageString.Append(element + "\n");
        }
        informationPackageText.text = "Information package" + "\n" + 
            "This information will be submitted. Click on an item to remove it." + "\n\n" + informationPackageString.ToString();

    }

}
