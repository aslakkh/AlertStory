using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
[CreateAssetMenu(fileName = "StoryEvent", menuName = "Alert/StoryEvent", order = 1)]
public class StoryEvent : ScriptableObject
{

    public string _text;
    public string _title;
    public List<Choice> _choices = new List<Choice>();
    public RequirementList requirements;
    public Dependencies dependencies;

    public StoryEvent(string text, List<Choice> choices, RequirementList requirements, Dependencies dependencies)
    {
        _text = text;
        _choices = choices;
        this.requirements = requirements;
        this.dependencies = dependencies;
    }

    public bool IsMultipleChoice()
    {
        return _choices.Count > 1;
    }


}