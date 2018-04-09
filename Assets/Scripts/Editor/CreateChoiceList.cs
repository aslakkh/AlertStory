using UnityEngine;
using UnityEditor;

public class CreateChoiceList {
    [MenuItem("Assets/Create/Event/ChoiceList")]
    public static ChoiceList Create() {
        ChoiceList asset = ScriptableObject.CreateInstance<ChoiceList>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObjects/Event/NewChoiceList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
    
}
