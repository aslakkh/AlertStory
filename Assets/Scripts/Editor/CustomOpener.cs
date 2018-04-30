using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomOpener {

    //allows for opening of custom assets in their custom editors via Unity Inspector
    [UnityEditor.Callbacks.OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject as Character != null) //open character editor
        {
            CharacterEditor.Init(Selection.activeObject as Character);
            return true; //catch open file
        }
        else if(Selection.activeObject as CharacterList != null) //open character list editor
        {
            CharacterListEditor.Init(Selection.activeObject as CharacterList);
            return true; //catch open file
        }
        else if(Selection.activeObject as FriendsbookProfile != null) //open friendsbookprofile editor
        {
            CharacterEditor.Init((Selection.activeObject as FriendsbookProfile).character);
            return true; //catch open file
        }
        else if(Selection.activeObject as StoryEvent != null) //open storyevent editor
        {
            StoryEventEditor.Init(Selection.activeObject as StoryEvent);
        }
        else if(Selection.activeObject as StoryEventList != null) //open storyeventlist editor
        {
            StoryEventListEditor.Init(Selection.activeObject as StoryEventList);
        }
        return false; // let unity open the file
    }
}
