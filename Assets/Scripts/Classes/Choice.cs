using UnityEngine;
using System;

[Serializable]
public class Choice{
    public string choiceDescription;
    public int affectScore;
    public int affectSecretScore;
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
