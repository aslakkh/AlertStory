using UnityEngine;
using System;
using Settings;
using System.Collections.Generic;

//score with setting requirement (will be applied if requirements match user settings)
[Serializable]
public class Score
{
    public Requirement requirement;
    public Setting setting;
    public int value;

    public Score()
    {
        requirement = null;
        setting = Setting.Public;
        value = 0;
    }

    public void SetSetting(Setting setting)
    {
        this.setting = setting;
    }

    public void SetRequirement(Requirement requirement)
    {
        this.requirement = requirement;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }
}

[Serializable]
public class Choice{
    public string choiceDescription;
    public int affectScorePrivate;
    public int affectScoreFriends;
    public int affectScorePublic;
    public List<Score> scores;
    public StoryEvent triggersStoryEventPrivate;
    public StoryEvent triggersStoryEventFriends;
    public StoryEvent triggersStoryEventPublic;
    
    
    public Choice() {
        choiceDescription = "Blank";
        affectScorePrivate = affectScoreFriends = affectScorePublic = 0;
        triggersStoryEventPrivate = triggersStoryEventFriends = triggersStoryEventPublic = null;
        scores = new List<Score>();
    }

    public void AddNewScore()
    {
        scores.Add(new global::Score());
    }


}
