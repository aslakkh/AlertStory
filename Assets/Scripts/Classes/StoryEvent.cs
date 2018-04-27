using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Test", menuName = "Alert/StoryEventTest", order = 1)]
public class StoryEvent : ScriptableObject {

    public string title;
    public string text;
    public RequirementDict requirements;
    public Dependencies dependencies;
    public List<Choice> choices;

    public void Init()
    {
        var req = ScriptableObject.CreateInstance<RequirementDict>();
        var dep = ScriptableObject.CreateInstance<Dependencies>();
        this.requirements = req;
        this.dependencies = dep;
        //AssetDatabase.AddObjectToAsset(req, this);
        //AssetDatabase.AddObjectToAsset(dep, this);
        //AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));

    }

    public bool IsMultipleChoice()
    {
        return choices.Count > 1;
    }
    
}
