using System;
using System.Collections.Generic;
using UnityEngine;


public class StoryEvent : ScriptableObject {

    public string title;
    public string text;
    public RequirementDict requirements;
    public Dependencies dependencies;
    [NonSerialized] public List<Choice> choices;

    public StoryEvent() {
        title = "";
        text = "";
        choices = new List<Choice>();
        requirements = new RequirementDict();
        dependencies = new Dependencies();
    }

    public StoryEvent(string title, string text, List<Choice> choices, RequirementDict requirements, Dependencies dependencies) {
        this.title = title;
        this.text = text;
        this.choices = choices;
        this.requirements = requirements;
        this.dependencies = dependencies;
    }

    public bool IsMultipleChoice()
    {
        return choices.Count > 1;
    }
    
}
