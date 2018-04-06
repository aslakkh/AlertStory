using UnityEngine;
using System;

[System.Serializable]
public class Choice{

    public string _choiceDescription;
    public int _affectScore;
    public int _affectSecretScore;
    public StoryEvent _triggersStoryEvent;
        
    public Choice(string choiceDescription, int affectScore, int affectSecretScore, StoryEvent triggersStoryEvent) {
        _choiceDescription = choiceDescription;
        _affectScore = affectScore;
        _affectSecretScore = affectSecretScore;
        _triggersStoryEvent = triggersStoryEvent;
    }

}
