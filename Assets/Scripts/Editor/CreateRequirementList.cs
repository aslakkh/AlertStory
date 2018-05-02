using UnityEngine;
using UnityEditor;

public class CreateRequirementList {
    [MenuItem("Assets/Create/Event/RequirementList")]
    public static RequirementsList Create() {
        return (Create(""));
    }

    public static RequirementsList Create(string title)
    {
        if (string.IsNullOrEmpty(title)) { title = "NewRequirementList"; }
        RequirementsList asset = ScriptableObject.CreateInstance<RequirementsList>();
        AssetDatabase.CreateAsset(asset, string.Format("Assets/Resources/ScriptableObjects/Events/{0}.asset", title));
        AssetDatabase.SaveAssets();
        return asset;
    }
}
