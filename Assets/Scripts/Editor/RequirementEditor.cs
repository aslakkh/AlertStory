using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class RequirementEditor : EditorWindow
{

    public RequirementsList requirementList;
    private int viewIndex = 1;

    [MenuItem("Window/Event/RequirementEditor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(RequirementEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath")) {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            requirementList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(RequirementsList)) as RequirementsList;
        }
       
    }

    void OnGUI()
    {
       
        GUILayout.BeginHorizontal();
        GUILayout.Label("Requirement Editor", EditorStyles.boldLabel);
        if (requirementList != null)
        {
            if (GUILayout.Button("Show Requirement List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = requirementList;
            }
        }
        if (GUILayout.Button("Open Requirement List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = requirementList;
        }
        GUILayout.EndHorizontal();

        if (requirementList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Requirement List", GUILayout.ExpandWidth(false)))
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

        if (requirementList != null)
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
                if (viewIndex < requirementList.list.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Requirement", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Requirement", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (requirementList.list != null && requirementList.list.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Card", viewIndex, GUILayout.ExpandWidth(false)), 1, requirementList.list.Count);
                //Mathf.Clamp (viewIndex, 1, itemTypeList.list.Count);
                EditorGUILayout.LabelField("of   " + requirementList.list.Count.ToString() + "  cards", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                requirementList.list[viewIndex - 1].requirementName = EditorGUILayout.TextField("RequirementName", requirementList.list[viewIndex - 1].requirementName as string);

                GUILayout.Space(10);
            }
            else
            {
                GUILayout.Label("This RequirementsList is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(requirementList);
            AssetDatabase.SaveAssets();
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        requirementList = CreateRequirementList.Create();
        if (requirementList)
        {
            requirementList.list = new List<Requirement>();
            string relPath = AssetDatabase.GetAssetPath(requirementList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select RequirementList asset", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            requirementList = AssetDatabase.LoadAssetAtPath(relPath, typeof(RequirementsList)) as RequirementsList;
            if (requirementList.list == null)
                requirementList.list = new List<Requirement>();
            if (requirementList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        Requirement newItem = new Requirement {requirementName = "New requirement"};
        requirementList.list.Add(newItem);
        viewIndex = requirementList.list.Count;
    }

    void DeleteItem(int index)
    {
        requirementList.list.RemoveAt(index);
    }
}