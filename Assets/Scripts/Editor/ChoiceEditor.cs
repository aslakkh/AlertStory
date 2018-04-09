﻿using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class ChoiceEditor : EditorWindow
{

    public ChoiceList choiceList;
    private int viewIndex = 1;
    private StoryEventList eventList;
    private int _itemIndex = 0;
    private List<string> typeListString;

    [MenuItem("Window/Event/ChoiceEditor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(ChoiceEditor));
    }

    private void OnFocus()
    {
        eventList = AssetDatabase.LoadAssetAtPath("Assets/Resources/ScriptableObjects/Events/EventList.asset", typeof(StoryEventList)) as StoryEventList;
        typeListString.Clear();
        if (eventList == null) return;
        foreach (StoryEvent item in eventList.list) {
            typeListString.Add(item.title);
        }
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath")) {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            choiceList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(ChoiceList)) as ChoiceList;
        }
       
    }

    void OnGUI()
    {
       
        GUILayout.BeginHorizontal();
        GUILayout.Label("Choice Editor", EditorStyles.boldLabel);
        if (choiceList != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = choiceList;
            }
        }
        if (GUILayout.Button("Open Item List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = choiceList;
        }
        GUILayout.EndHorizontal();

        if (choiceList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Choice List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (choiceList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < choiceList.list.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (choiceList.list != null && choiceList.list.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Card", viewIndex, GUILayout.ExpandWidth(false)), 1, choiceList.list.Count);
                //Mathf.Clamp (viewIndex, 1, itemTypeList.list.Count);
                EditorGUILayout.LabelField("of   " + choiceList.list.Count.ToString() + "  cards", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                choiceList.list[viewIndex - 1].choiceDescription = EditorGUILayout.TextField("Choice Description", choiceList.list[viewIndex - 1].choiceDescription as string);

                GUILayout.Space(10);

                _itemIndex = EditorGUILayout.Popup("Event", _itemIndex, typeListString.ToArray());

                choiceList.list[viewIndex - 1].triggersStoryEvent = eventList.list[_itemIndex];            
                
                GUILayout.Space(10);

                choiceList.list[viewIndex - 1].affectScore = EditorGUILayout.IntField("Affects Score", choiceList.list[viewIndex - 1].affectScore);

                GUILayout.Space(10);
                
                choiceList.list[viewIndex - 1].affectSecretScore = EditorGUILayout.IntField("Affects Hidden Score", choiceList.list[viewIndex - 1].affectSecretScore);
                
                GUILayout.Space(10);

            }
            else
            {
                GUILayout.Label("This ChoiceList is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(choiceList);
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        choiceList = CreateChoiceList.Create();
        if (choiceList)
        {
            choiceList.list = new List<Choice>();
            string relPath = AssetDatabase.GetAssetPath(choiceList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select ChoiceList", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            choiceList = AssetDatabase.LoadAssetAtPath(relPath, typeof(ChoiceList)) as ChoiceList;
            if (choiceList.list == null)
                choiceList.list = new List<Choice>();
            if (choiceList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        Choice newItem = new Choice();
        newItem.choiceDescription = "New choice";
        choiceList.list.Add(newItem);
        viewIndex = choiceList.list.Count;
    }

    void DeleteItem(int index)
    {
        choiceList.list.RemoveAt(index);
    }
}