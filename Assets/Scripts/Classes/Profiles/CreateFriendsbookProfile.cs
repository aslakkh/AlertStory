using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFriendsbookProfile {

	public static FriendsbookProfile Create()
    {
        FriendsbookProfile asset = ScriptableObject.CreateInstance<FriendsbookProfile>();
        asset.Init();


        return asset;
    }
}
