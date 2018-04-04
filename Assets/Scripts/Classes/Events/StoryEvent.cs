using UnityEngine;
using System.Collections.Generic;
using System;

public class StoryEvent : MonoBehaviour {

    public string _text;
    public string _title;
    public List<Choice> _choices = new List<Choice>();
    public RequirementList requirements;
    public Dependencies dependencies;

    //public StoryEvent(string text, string title, List<Choice> choices, RequirementList requirements, Dependencies dependencies) {
    //    _text = text;
    //    _title = title;
    //    _choices = choices;
    //    this.requirements = requirements;
    //    this.dependencies = dependencies;
    //}

    public bool IsMultipleChoice()
    {
        return _choices.Count > 1;
    }

    
}
