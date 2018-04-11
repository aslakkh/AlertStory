using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotificationBar : MonoBehaviour, IPointerClickHandler {

    public Text stats;
    public Text objectives;

    public GameObject dropDown;
    private GameManager gameManager;

    void Awake()
    {
        Image image = dropDown.GetComponent<Image>();
        dropDown.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        stats = dropDown.transform.Find("Stats").GetComponent<Text>();
        objectives = dropDown.transform.Find("Objectives").GetComponent<Text>();
        Debug.Log(gameManager.score.ToString());
        GameManager.Instance.score = 10;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        stats.text = "Score: " + GameManager.Instance.score.ToString();
        objectives.text = "Objectives: ";
        if (dropDown.activeSelf)
        {
            dropDown.SetActive(false);
            dropDown.transform.SetAsFirstSibling();
        }
        else
        {
            dropDown.SetActive(true);
            dropDown.transform.SetAsLastSibling();
            //GUI.BringWindowToFront();
        }


    }

}
