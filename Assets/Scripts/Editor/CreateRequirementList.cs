using UnityEngine;
using UnityEditor;

public class CreateRequirementList {
    [MenuItem("Assets/Create/Event/Requirement")]
    public static RequirementsList Create() {
        RequirementsList asset = ScriptableObject.CreateInstance<RequirementsList>();
        asset.Init();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObjects/Event/NewRequirementList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
