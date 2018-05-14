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
    public GameObject informationPackageItem;
    public Transform informationSpawn;
    private Dictionary<int, List<Objective>> objectivesList;
    private StringSettingDictionary settingsList;
    private Requirement requirement;
    private List<string> informationPackage;


    public GameObject scrollView;
    public GameObject dropDown;
    private GameManager gameManager;

    //colors used to differentiate list items in information package
    private Color informationPackageListItemsColor1;
    private Color informationPackageListItemsColor2;

    void Start()
    {
        settingsList = GameManager.Instance.requirements;
        informationPackage = GameManager.Instance.informationPackage;
        objectivesList = GameManager.Instance.objectives;
        stats.text = "Score: " + gameManager.score.ToString();

        informationPackageListItemsColor2 = new Color(0.8f, 0.8f, 0.8f, 1);
        informationPackageListItemsColor1 = new Color(1, 1, 1, 1);
    }

    void Awake()
    {
        scrollView.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.stateChanged += OnStateChanged; //subscribe to stateChanged event
        stats = GameObject.Find("NotificationBar").transform.GetChild(0).GetComponent<Text>();
        objectives = dropDown.transform.Find("Objectives").GetComponent<Text>();
    }


    //subscriber to state change event in gamemanager
    private void OnStateChanged(object source, GameStateEventArgs args)
    {
        //update score if state is set to investigator
        if(args.newState == GameState.investigator)
        {
            stats.text = "Score: " + gameManager.score.ToString();
        }
    }

    //remove subscriber from gamemanager
    private void OnDestroy()
    {
        gameManager.stateChanged -= OnStateChanged; //subcribe to stateChanged event
    }

    public void OnListElementClick(GameObject obj)
    {
        string temp = obj.transform.GetChild(0).GetComponent<Text>().text;
        for (int i = 0; i < informationPackage.Count; i += 2)
        {
            if (informationPackage[i] == temp)
            {
                informationPackage.RemoveRange(i, 2);
            }
        }
        GameObject.Destroy(obj);
    }


    //Opens dropdown
    public void OnPointerClick(PointerEventData eventData)
    {
        //Sets score string
        stats.text = "Score: " + gameManager.score;

        if (scrollView.activeSelf)
        {
            // Destroys any information package items when hiding the scrollview
            foreach(Transform child in informationSpawn)
            {
                GameObject.Destroy(child.gameObject);
            }
            scrollView.SetActive(false);
            scrollView.transform.SetAsFirstSibling();
        }
        else
        {
            scrollView.SetActive(true);
            scrollView.transform.SetAsLastSibling();

            //Sets objectives string
            StringBuilder objectivesString = new StringBuilder();
            foreach (Objective o in objectivesList[gameManager.GetDayCount()])
            {

                objectivesString.Append(o.description + "\n");
            }
            objectives.text = "Objectives: " + "\n" + objectivesString.ToString();
            //Sets settings string 
            settings.text = "Settings: " + "" + settingsList["Facebook"];

            informationPackage = GameManager.Instance.informationPackage;
            for (int i = 0; i < informationPackage.Count; i += 2)
            {
                GameObject temp = GameObject.Instantiate(informationPackageItem);
                temp.transform.SetParent(informationSpawn, false);
                temp.GetComponent<Image>().color = (i % 4 == 0) ? informationPackageListItemsColor1 : informationPackageListItemsColor2;
                temp.transform.GetChild(0).GetComponent<Text>().text = informationPackage[i];
                temp.transform.GetChild(1).GetComponent<Text>().text = informationPackage[i + 1];
                temp.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    OnListElementClick(temp);
                });

            }
        }
    }

}
