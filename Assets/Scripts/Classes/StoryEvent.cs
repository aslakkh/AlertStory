using System;
using System.Collections.Generic;

[Serializable]
public class StoryEvent {

    public string title;
    public string text;
    public List<Choice> choices;
    public RequirementDict requirements;
    public Dependencies dependencies;

    public List<Choice> Choices {
        get { return choices; }
        set { choices = value; }
    }

    public StoryEvent() {
        this.title = "";
        this.text = "";
        this.choices = null;
        this.requirements = new RequirementDict();
        this.dependencies = new Dependencies();
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
