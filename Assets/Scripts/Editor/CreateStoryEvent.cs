using UnityEngine;
using UnityEditor;

public class CreateStoryEvent : MonoBehaviour {
    [MenuItem("Assets/Create/Event/StoryEventList")]
    public static StoryEventList Create() {
        StoryEventList asset = ScriptableObject.CreateInstance<StoryEventList>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObjects/Event/NewStoryEventList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
