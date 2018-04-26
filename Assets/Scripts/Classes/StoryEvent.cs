
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "TEST", menuName = "Alert/test", order = 1)]
public class StoryEvent : ScriptableObject
{

    public string title;
    public string text;
    //public RequirementDict requirements;
    public List<StoryEvent> dependencies;
    public List<Choice> choices;

    public bool IsMultipleChoice()
    {
        return choices.Count > 1;
    }
}
