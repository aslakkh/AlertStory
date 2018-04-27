using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomOpener {

    //allows for opening of custom assets in their custom editors via Unity Inspector
    [UnityEditor.Callbacks.OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject as Character != null)
        {
            CharacterEditor.Init(Selection.activeObject as Character);
            return true; //catch open file
        }
        else if(Selection.activeObject as CharacterList != null)
        {
            CharacterListEditor.Init(Selection.activeObject as CharacterList);
            return true; //catch open file
        }
        else if(Selection.activeObject as FriendsbookProfile != null)
        {
            CharacterEditor.Init((Selection.activeObject as FriendsbookProfile).character);
            return true; //catch open file
        }
        return false; // let unity open the file
    }
}
