using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Test", menuName = "Alert/StoryEventTest", order = 1)]
public class StoryEvent : ScriptableObject {

    public string title;
    public string text;
    public bool isMessage;
    public RequirementDict requirements;
    public Dependencies dependencies;
    public List<Choice> choices;

    public void Init(string title)
    {
        SetTitle(title);
        choices = new List<Choice>();
        choices.Add(new global::Choice());
    }

    public void SetTitle(string title)
    {
        this.title = title;
    }

    public bool IsMultipleChoice()
    {
        return choices.Count > 1;
    }
    
}
