using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateFriendsbookPostList
{
    public static FriendsbookPostList Create(string path, string fullName)
    {
        //creates character with filename firstName+lastName.asset
        //character is placed in new folder
        FriendsbookPostList asset = ScriptableObject.CreateInstance<FriendsbookPostList>();
        asset.Init();
        fullName = fullName.Replace(" ", ""); //strip whitespace
        AssetDatabase.CreateAsset(asset, string.Format("{0}/{1}/{1}FriendsbookPosts.asset", path, fullName));
        AssetDatabase.SaveAssets();
        return asset;
    }

}