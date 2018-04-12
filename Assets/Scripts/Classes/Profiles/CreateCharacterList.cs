using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCharacterList
{
    //[MenuItem("Assets/Create/Alert/CharacterList")]
    public static CharacterList Create(string title)
    {
        if(string.IsNullOrEmpty(title))
        {
            title = "NewCharacterList";
        }
        CharacterList asset = ScriptableObject.CreateInstance<CharacterList>();
        asset.Init();
        AssetDatabase.CreateFolder("Assets/Resources/ScriptableObjects/Characters", title);
        AssetDatabase.CreateAsset(asset, string.Format("Assets/Resources/ScriptableObjects/Characters/{0}/{0}.asset", title));
        AssetDatabase.SaveAssets();
        return asset;
    }
}