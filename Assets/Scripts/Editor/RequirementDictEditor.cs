using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Settings;
using System;

public class RequirementDictEditor : EditorWindow {

    private RequirementDict requirementDict; //requirementDict being edited;
    private RequirementsList requirementList; //requirementList used to add requirements
    private int _reqIndex = 0;
    private List<string> requirementListString = new List<string>();
    private string requirementPath;

    private string newRequirementDictName;
    private Vector2 mainScrollPosition;

    //init being called on opening via menu
    [MenuItem("Window/Alert/RequirementDictEditor")]
	static void Init()
    {
        EditorWindow.GetWindow(typeof(RequirementDictEditor));
    }

    //init being called when opening via open-button
    public static void Init(RequirementDict requirementDict)
    {
        RequirementDictEditor w = EditorWindow.GetWindow<RequirementDictEditor>();
        w.requirementDict = requirementDict;
    }

    private void OnFocus()
    {
        GUI.FocusControl(null);
    }

    private void OnGUI()
    {

        //entire window should be scrollable
        mainScrollPosition = GUILayout.BeginScrollView(mainScrollPosition);
        if (requirementDict == null)
        {
            //allow user to open or create requirementdict
            if (GUILayout.Button("Open", GUILayout.ExpandWidth(false)))
            {
                OpenRequirementDict();
            }

            newRequirementDictName = EditorGUILayout.TextField("New RequirementDict: ", newRequirementDictName);
            if (GUILayout.Button("Create new", GUILayout.ExpandWidth(false)))
            {
                CreateNewRequirementDict();
            }

        }
        else
        {
            //Open finder if no requirements asset is loaded
            if (requirementList == null)
            {
                GUILayout.Label("Choose a requirementList to add requirements from", EditorStyles.helpBox);
                if (GUILayout.Button("Open requirement list", GUILayout.ExpandWidth(false)))
                {
                    OpenRequirementList();
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                // Create popup and add to list
                _reqIndex = EditorGUILayout.Popup("Add Requirement", _reqIndex,
                    requirementListString.ToArray());
                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                {
                    requirementDict.Add(requirementList.list[_reqIndex].requirementName, Setting.Public);
                    EditorUtility.SetDirty(requirementDict);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(3);
                if (GUILayout.Button("Select other requirementlist", GUILayout.ExpandWidth(false)))
                {
                    OpenRequirementList();
                }
            }

            GUILayout.Label("Requirements in dict: ", EditorStyles.largeLabel);
            if (requirementDict.requirementDictionary != null && requirementDict.requirementDictionary.Count > 0)
            {
                //allow user to edit requirementDict
                for (int i = 0; i < requirementDict.requirementDictionary.Count; i++)
                {
                    var item = requirementDict.requirementDictionary.ElementAt(i);
                    GUILayout.BeginHorizontal();

                    //Dispalys name as Static and boolean as interchangable
                    GUILayout.Label(item.Key);
                    var value = (Setting)EditorGUILayout.EnumPopup("Setting", item.Value);

                    //Update only on value change check
                    if (value != item.Value)
                    {
                        requirementDict.UpdateValue(item.Key, value);
                        EditorUtility.SetDirty(requirementDict);
                    }
                    //Delete Button for choice, only in relevant story.
                    if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                    {
                        requirementDict.Remove(item.Key);
                        EditorUtility.SetDirty(requirementDict);
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }
            }
            else
            {
                GUILayout.Label("No requirements in requirementDict", EditorStyles.boldLabel);

            }
            

        }
        GUILayout.EndScrollView();
    }

    private void CreateNewRequirementDict()
    {
        requirementDict = CreateRequirementDict.Create(newRequirementDictName);
    }

    private void OpenRequirementDict()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Requirementdict", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            requirementDict = AssetDatabase.LoadAssetAtPath(relPath, typeof(RequirementDict)) as RequirementDict;
            if (requirementDict)
            {
                EditorPrefs.SetString("ObjectPath", relPath);

                //init editable strings
                //SetEventInfoValues();
            }
        }
    }

    void OpenRequirementList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select RequirementList", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            requirementPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SetRequirementsList(requirementPath);
        }
    }

    //Helper function to set requiremenlist after update.
    void SetRequirementsList(string path)
    {
        requirementList = AssetDatabase.LoadAssetAtPath(path, typeof(RequirementsList)) as RequirementsList;
        if (requirementListString != null)
        {
            requirementListString.Clear();
        }

        if (requirementList == null) return;
        foreach (Requirement item in requirementList.list)
        {
            requirementListString.Add(item.requirementName);
        }
    }
}
