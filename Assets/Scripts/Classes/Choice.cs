using UnityEngine;
using System;
using Settings;
using System.Collections.Generic;

//score with setting requirement (will be applied if requirements match user settings)
[Serializable]
public class Score
{
    public string requirementName;
    public Setting setting;
    public int value;

    public Score()
    {
        requirementName = null;
        setting = Setting.Public;
        value = 0;
    }

    public void SetSetting(Setting setting)
    {
        this.setting = setting;
    }

    public void SetRequirementName(string requirementName)
    {
        this.requirementName = requirementName;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }
}

[Serializable]
public class Choice{
    public string choiceDescription;
    public List<Score> scores;
    public bool endGameTrigger;
    
    public Choice() {
        choiceDescription = "Blank";
        scores = new List<Score>();
        endGameTrigger = false;
    }

    public void AddNewScore()
    {
        scores.Add(new global::Score());
    }


}
