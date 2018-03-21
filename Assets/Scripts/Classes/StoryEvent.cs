using UnityEngine;
using System.Collections.Generic;
using System;

public class StoryEvent : MonoBehaviour {

    private string _text { get; }
    private List<Choice> _choices { get; }
    protected RequirementList requirements { get; }
    protected Dependencies dependencies { get; }

    public StoryEvent(string text, List<Choice> choices, RequirementList requirements, Dependencies dependencies) {
        _text = text;
        _choices = choices;
        this.requirements = requirements;
        this.dependencies = dependencies;
    }

    
}
