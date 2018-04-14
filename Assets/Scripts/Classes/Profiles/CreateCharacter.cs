using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCharacter
{
    public static Character Create(CharacterList c, string fullName, string path)
    {
        //creates character with filename firstName+lastName.asset
        //character is placed in new folder
        Character asset = ScriptableObject.CreateInstance<Character>();
        asset.Init(c, fullName);
        fullName = fullName.Replace(" ", ""); //strip whitespace
        AssetDatabase.CreateFolder(path, (fullName));
        AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}/{1}Character.asset", path, fullName));
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static void Delete()
    {

    }
}