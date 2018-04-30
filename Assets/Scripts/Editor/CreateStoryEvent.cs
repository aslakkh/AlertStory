using UnityEngine;
using UnityEditor;

public class CreateStoryEvent : MonoBehaviour {

    //storyeventlist creators

    [MenuItem("Assets/Create/Event/StoryEventList")]
    public static StoryEventList CreateStoryEventList() {
        StoryEventList asset = ScriptableObject.CreateInstance<StoryEventList>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObjects/Event/NewStoryEventList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static StoryEventList CreateStoryEventList(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            title = "NewStoryEventList";
        }
        StoryEventList asset = ScriptableObject.CreateInstance<StoryEventList>();
        AssetDatabase.CreateFolder("Assets/Resources/ScriptableObjects/Events", title);
        AssetDatabase.CreateAsset(asset, string.Format("Assets/Resources/ScriptableObjects/Events/{0}/{0}.asset", title));
        AssetDatabase.SaveAssets();
        return asset;
    }

    //story event creators
    [MenuItem("Assets/Create/Event/StoryEvent")]
    public static StoryEvent CreateStoryEventAsset()
    {
        StoryEvent asset = ScriptableObject.CreateInstance<StoryEvent>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObjects/Event/NewStoryEvent.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static StoryEvent CreateStoryEventAsset(string name, string path)
    {
        StoryEvent asset = ScriptableObject.CreateInstance<StoryEvent>();
        AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}.asset", path, name));
        AssetDatabase.SaveAssets();
        return asset;
    }
}
