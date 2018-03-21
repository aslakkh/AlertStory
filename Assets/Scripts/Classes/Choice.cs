using UnityEngine;
using System;

public class Choice{

    public string _choiceDescription { get; set; }
    public int _affectScore { get; private set; }
    public int _affectSecretScore { get; private set; }
    public StoryEvent _triggersStoryEvent { get; private set; }
        
    public Choice(string choiceDescription, int affectScore, int affectSecretScore, StoryEvent triggersStoryEvent) {
        _choiceDescription = choiceDescription;
        _affectScore = affectScore;
        _affectSecretScore = affectSecretScore;
        _triggersStoryEvent = triggersStoryEvent;
    }

}
