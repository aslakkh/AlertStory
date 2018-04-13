﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCharacter
{
    //[MenuItem("Assets/Create/Alert/Character")]
    public static Character Create(string firstName, string lastName, string path)
    {
        
        Character asset = ScriptableObject.CreateInstance<Character>();
        asset.Init(firstName, lastName);
        Debug.Log(path);
        AssetDatabase.CreateFolder(path, (firstName+lastName));

        AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}/{1}Character.asset", path, firstName+lastName));
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static void Delete()
    {

    }
}