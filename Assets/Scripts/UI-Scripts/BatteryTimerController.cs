using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.UI;

public class BatteryTimerController : MonoBehaviour {
    public float timeLenght = 0f;
    public float timeStart;
    public float batteryPrecentage = 100f;
    public List<Sprite> imageList;
    public List<float> eventBreakpoints;
    public Text text;
    public Image imageHolder;
    public bool runTimer = false;
    public float warningThreshold = 10f;
    public GameObject warningPanel; //prefab for warningpanel
    public Transform warningPanelSpawnPoint; //where to instantiate prefab
    private bool warned = false;
    private GameManager gameManager;
    

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.stateChanged += OnStateChanged; //subcribe to stateChanged event
    }

    public void OnStateChanged(object source, GameStateEventArgs args) {
        if (args.newState == GameState.investigator) {
            StartTimer();
        }
        else {
            Time.timeScale = 0;
        }
    }

    // Use this for initialization
    void Start() {
        //Set battery image to correnspond with starting battery
        imageHolder.sprite = imageList.SingleOrDefault(item => (item.name.Equals("100_battery")));
        StartTimer();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (runTimer) {
            if (batteryPrecentage > 0) {
                UpdateTimer();
                if (eventBreakpoints.Contains(batteryPrecentage)) {
                    eventBreakpoints.Remove(batteryPrecentage);
                    gameManager.FireEvent();
                }

                if (warned || !(Math.Abs(batteryPrecentage - warningThreshold) < 1f)) return;
                warned = true;
                var timeRemaining = timeLenght * (batteryPrecentage / 100);
                string timeRemainingText = Mathf.Floor(timeRemaining).ToString();
                DisplayWarning(timeRemainingText);
            }
            else {
                runTimer = false;
                gameManager.NextDay();
            }
        }
    }

    private void StartTimer() 
    {
        //If runTimer == True and StartTimer has been called, it should be to unpause
        if (runTimer) 
        {
            Time.timeScale = 1;
        }
        else 
        {
            //Set new timeStart
            timeStart = Time.time;
            batteryPrecentage = 100;
            runTimer = true;
        }
    }

    private void UpdateTimer() 
    {
        float t = Time.time - timeStart;
        batteryPrecentage = 100f - Mathf.Floor(t / timeLenght * 100);
        if (Mathf.FloorToInt(batteryPrecentage) % 10 == 0) {
            imageHolder.sprite = imageList.SingleOrDefault(item =>
                (item.name.Equals(Mathf.FloorToInt(batteryPrecentage).ToString() + "_battery")));
        }
    }

    //instantiate warning in scene
    private void DisplayWarning(string timeRemainingText)
    {
        var panel = GameObject.Instantiate(warningPanel);
        panel.transform.SetParent(warningPanelSpawnPoint, false); //recommended way of setting parent of UI element
        panel.transform.SetAsLastSibling(); //always on top
        panel.transform.Find("TimerText").GetComponent<Text>().text = timeRemainingText;
    }

    public void OnDestroy()
    {
        gameManager.stateChanged -= OnStateChanged;
    }
}

