using UnityEngine;
using System;

[Serializable]
public class Choice{
    public string choiceDescription { get; set; }
    public int affectScore { get; set; }
    public int affectSecretScore { get; set; }
    [SerializeField]
    public StoryEvent triggersStoryEvent;
    
    public StoryEvent TriggersStoryEvent {
        get { return triggersStoryEvent; }
        set { triggersStoryEvent = value; }
    }
    
    public Choice() {
        this.choiceDescription = "Blank";
        this.affectScore = 0;
        this.affectSecretScore = 0;
        this.triggersStoryEvent = null;
    }

    public Choice(string choiceDescription, int affectScore, int affectSecretScore, StoryEvent triggersStoryEvent) {
        this.choiceDescription = choiceDescription;
        this.affectScore = affectScore;
        this.affectSecretScore = affectSecretScore;
        this.triggersStoryEvent = triggersStoryEvent;
    }

}
