using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BatteryTimerController : MonoBehaviour {
    public float timeLenght = 0f;
    public float timeStart;
    public float batteryPrecentage = 100f;
    public List<Sprite> imageList;
    public Text text;
    public Image imageHolder;
    private bool runTimer = false;
    private GameManager gameManager;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.stateChanged += OnStateChanged; //subcribe to stateChanged event
    }

    public void OnStateChanged(object source, GameStateEventArgs args) {
        if (args.newState == GameState.investigator) {
            StartTimer();
        }
        else //buttons should only be interactable in investigator state
        {
            PauseTimer();
        }
    }

    // Use this for initialization
    void Start() {
        //Set battery image to correnspond with starting battery
        imageHolder.sprite = imageList.SingleOrDefault(item => (item.name.Equals(RoundUp(Mathf.FloorToInt(batteryPrecentage)).ToString() + "_battery")));
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (runTimer) {
            if (batteryPrecentage > 0) {
                UpdateTimer();
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
            runTimer = true;
        }
    }

    private void PauseTimer() 
    {
        if (runTimer) 
            {
            Time.timeScale = 0;
        }
    }

    private void UpdateTimer() 
    {
        float t = Time.time - timeStart;
        batteryPrecentage = 100f - Mathf.Floor(t / timeLenght * 100);
        imageHolder.sprite = imageList.SingleOrDefault(item => (item.name.Equals(RoundUp(Mathf.FloorToInt(batteryPrecentage)).ToString() + "_battery")));
    }

    //Helper function to have the sprite display upper sprite value
    private int RoundUp(int toRound) {
        if (toRound % 10 == 0) return toRound;
        return (10 - toRound % 10) + toRound;
    }
}

