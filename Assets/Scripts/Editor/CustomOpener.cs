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
            return true; //catch open file
        }
        else if(Selection.activeObject as StoryEventList != null) //open storyeventlist editor
        {
            StoryEventListEditor.Init(Selection.activeObject as StoryEventList);
            return true; //catch open file
        }
        else if(Selection.activeObject as RequirementDict != null) //open requirementDict editor
        {
            RequirementDictEditor.Init(Selection.activeObject as RequirementDict);
            return true; //catch open file
        }
        else if (Selection.activeObject as RequirementsList != null) //open open requirementList editor
        {
            RequirementEditor.Init(Selection.activeObject as RequirementsList);
            return true; //catch open file
        }
        return false; // let unity open the file
    }
}
