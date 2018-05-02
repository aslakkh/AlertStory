using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateRequirementDict {

    //create nameless requirementDict
    [MenuItem("Assets/Create/Alert/RequirementDict")]
    public static RequirementDict Create()
    {
       return Create("");
    }

    public static RequirementDict Create(string title)
    {
        if (string.IsNullOrEmpty(title)) { title = "NewRequirementList"; }
        RequirementDict asset = ScriptableObject.CreateInstance<RequirementDict>();
        AssetDatabase.CreateAsset(asset, string.Format("Assets/Resources/ScriptableObjects/Events/{0}.asset", title));
        AssetDatabase.SaveAssets();
        return asset;
    }
}
