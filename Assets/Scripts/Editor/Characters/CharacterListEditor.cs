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

    string fullName;
    string charactersSearchString = "";

    public Vector2 scrollPosition;

    [MenuItem("Window/Alert/Character/CharacterListEditor")]
    public static void Init()
    {
        EditorWindow.GetWindow<CharacterListEditor>();
        //EditorWindow.GetWindow<CharacterListEditor>(Type_to_tab_next_to);
    }

    public static void Init(CharacterList l)
    {
        var w = EditorWindow.GetWindow<CharacterListEditor>();
        w.characterList = l;
        string folderPath = AssetDatabase.GetAssetPath(l);
        w.folderPath = folderPath.Substring(0, folderPath.LastIndexOf("/"));
    }

    private void OnFocus()
    {
        GUI.FocusControl(null);
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
            fullName = EditorGUILayout.TextField("Full name", fullName);
            if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
            {
                Character c = CreateNewCharacter();
                EditorUtility.SetDirty(characterList);
                EditCharacter(c);
            }

            GUILayout.Label("Add Existing character", EditorStyles.boldLabel);
            if (GUILayout.Button("Find", GUILayout.ExpandWidth(false)))
            {
                //add existing character
            }

            GUILayout.Label("Characters", EditorStyles.boldLabel);

            //Search
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("Search by name: ");
            charactersSearchString = EditorGUILayout.TextField(charactersSearchString);
            GUILayout.EndHorizontal();

            //filter characters on searchstring
            List<Character> filteredList = characterList.list.FindAll(c => c.fullName.ToLower().Contains(charactersSearchString.ToLower()));

            if(filteredList.Count == 0)
            {
                GUILayout.Label("None found.");
            }
            else
            {
                //wraps character list in scrollview
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                for (int i = 0; i < filteredList.Count; i++) //inefficient?
                {
                    Character c = filteredList[i];
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(c.fullName);
                    if (GUILayout.Button("Edit", GUILayout.ExpandWidth(false)))
                    {
                        EditCharacter(c);
                    }
                    if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                    {
                        RemoveCharacter(i);
                    }

                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            
            
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
                folderPath = relPath.Substring(0, relPath.LastIndexOf("/"));
            }
        }
    }

    Character CreateNewCharacter()
    {
        Character c = CreateCharacter.Create(characterList, fullName, folderPath);
        characterList.AddCharacter(c);
        return c;
    }

    void RemoveCharacter(int i)
    {
        //ConfirmationWindow.Init("Are you sure you want to permanently delete this character?");
        characterList.RemoveCharacter(i);
    }

    void EditCharacter(Character c)
    {
        CharacterEditor.Init(c);
    }
}
