using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateFriendsbookProfile {

	public static FriendsbookProfile Create(Character c, string path, string name)
    {
        FriendsbookProfile asset = ScriptableObject.CreateInstance<FriendsbookProfile>();
        asset.Init(c);
        name = name.Replace(" ", ""); //strip whitespace
        AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}/{1}Friendsbook.asset", path, name));
        AssetDatabase.SaveAssets();

        return asset;
    }
}
