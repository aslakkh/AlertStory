using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public class NotificationBar : MonoBehaviour, IPointerClickHandler {

    public Text stats;
    public Text objectives;
    private Image image;
    private List<string> objectivesList;

    public GameObject dropDown;
    private GameManager gameManager;

    void Awake()
    {
        image = dropDown.GetComponent<Image>();
        dropDown.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stats = dropDown.transform.Find("Stats").GetComponent<Text>();
        objectives = dropDown.transform.Find("Objectives").GetComponent<Text>();
        GameManager.Instance.score = 10;
        GameManager.Instance.objectives = new List<string> { "Find personal data, Become friends with Ola Nordmann" };
        objectivesList = GameManager.Instance.objectives;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        stats.text = "Score: " + GameManager.Instance.score.ToString();
        objectives.text = "Objectives: " + GameManager.Instance.objectives.ToString();
        StringBuilder sb = new StringBuilder();
        foreach (string objective in objectivesList)
        {
            sb.Append(objective + ", " + "\n");
        }
        objectives.text = "Objectives: " + sb.ToString();
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
