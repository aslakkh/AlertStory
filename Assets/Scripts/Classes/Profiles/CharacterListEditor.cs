using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConfirmationWindow : EditorWindow
{
    public string textString;

    public static void Init(string s)
    {
        ConfirmationWindow w = EditorWindow.GetWindow<ConfirmationWindow>();
        w.textString = s;

    }

    private void OnGUI()
    {
        GUILayout.Label(textString, EditorStyles.boldLabel);
        if (GUILayout.Button("Confirm", GUILayout.ExpandWidth(false)))
        {
            Confirm();
        }
        if (GUILayout.Button("Cancel", GUILayout.ExpandWidth(false)))
        {
            Cancel();
        }
    }

    void Confirm()
    {

    }

    void Cancel()
    {

    }
}

public class CharacterListEditor : EditorWindow {
    public CharacterList characterList;

    string characterListTitle;
    string folderPath;

    string firstName;
    string lastName;

    public Vector2 scrollPosition;

    [MenuItem("Window/Alert/Character/CharacterListEditor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(CharacterListEditor));
        //EditorWindow.GetWindow<CharacterListEditor>(Type_to_tab_next_to);
    }

    private void OnGUI()
    {
        if(characterList == null) //GUI for creating new or opening existing CharacterList
        {
            
            GUILayout.Label("Open Existing List", EditorStyles.boldLabel);
            if (GUILayout.Button("Open", GUILayout.ExpandWidth(false)))
            {
                OpenCharacterList();
            }
            GUILayout.Label("Create new list", EditorStyles.boldLabel);
            GUILayout.Label("WARNING: No overwrite protection!", EditorStyles.helpBox);
            //GUILayout.Space(10);
            characterListTitle = EditorGUILayout.TextField("New Character List", characterListTitle);
            if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
            {
                CreateNewList();
            }
        }


        else //GUI for editing existing CharacterList
        {
            GUILayout.Label("Add new character", EditorStyles.boldLabel);
            firstName = EditorGUILayout.TextField("First name", firstName);
            lastName = EditorGUILayout.TextField("Last name", lastName);
            if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
            {
                CreateNewCharacter();
            }

            GUILayout.Label("Add Existing character", EditorStyles.boldLabel);
            if (GUILayout.Button("Find", GUILayout.ExpandWidth(false)))
            {
                //add existing character
            }

            GUILayout.Label("Characters", EditorStyles.boldLabel);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(300), GUILayout.Height(300));
            for(int i = 0; i < characterList.list.Count; i++) //inefficient?
            {
                Character c = characterList[i];
                GUILayout.BeginHorizontal();
                GUILayout.Label(string.Format("{0} {1}", c.firstName, c.lastName));
                if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                {
                    RemoveCharacter(i);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }


    }

    //creates new characterlist with title characterListTitle
    //TODO: Overwrite protection
    void CreateNewList()
    {
        CreateCharacterList.Create(characterListTitle);
    }

    void OpenCharacterList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select CharacterList", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            characterList = AssetDatabase.LoadAssetAtPath(relPath, typeof(CharacterList)) as CharacterList;
            if (characterList.list == null) characterList.list = new List<Character>();
            if (characterList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
                folderPath = relPath.Substring(0, relPath.LastIndexOf("/")+1);
            }
        }
    }

    void CreateNewCharacter()
    {
        Character c = CreateCharacter.Create(firstName, lastName, folderPath);
        characterList.AddCharacter(c);
    }

    void RemoveCharacter(int i)
    {
        //ConfirmationWindow.Init("Are you sure you want to permanently delete this character?");
        characterList.RemoveCharacter(i);
    }
}
