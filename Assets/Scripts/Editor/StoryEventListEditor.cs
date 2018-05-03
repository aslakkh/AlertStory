using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StoryEventListEditor : EditorWindow
{

    private StoryEventList storyEventList; //storyEventList being edited
    private string folderPath; //path to folder of storyEventList

    //gui variables
    private string storyEventListTitle;
    private Vector2 scrollPosition;
    private string storyEventTitle;
    Vector2 mainScrollPosition;

    [MenuItem("Window/Alert/Events/StoryEventList")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(StoryEventListEditor));
    }

    public static void Init(StoryEventList l)
    {
        var w = EditorWindow.GetWindow<StoryEventListEditor>();
        w.storyEventList = l;
        string folderPath = AssetDatabase.GetAssetPath(l);
        w.folderPath = folderPath.Substring(0, folderPath.LastIndexOf("/"));
    }

    private void OnFocus()
    {
        GUI.FocusControl(null);
    }

    private void OnGUI()
    {
        //entire window should be scrollable
        mainScrollPosition = GUILayout.BeginScrollView(mainScrollPosition);

        //check for storyEventList
        if (storyEventList == null)
        {
            //let user open existing or create new list
            storyEventListTitle = EditorGUILayout.TextField("New StoryEventList", storyEventListTitle);
            if (GUILayout.Button("Create New StoryEvent List", GUILayout.ExpandWidth(false)))
            {
                CreateEventStoryList(storyEventListTitle);
            }
            if (GUILayout.Button("Open Existing StoryEvent List", GUILayout.ExpandWidth(false)))
            {
                OpenStoryEventList();
            }
        }

        else
        {
            GUILayout.Label("Create New Event", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            //create new storyevent
            storyEventTitle = EditorGUILayout.TextField("New story event title", storyEventTitle);
            if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
            {
                CreateNewStoryEvent();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("Add Existing Event", EditorStyles.boldLabel);

            if (GUILayout.Button("Open", GUILayout.ExpandWidth(false)))
            {
                StoryEvent s = OpenStoryEvent();
                if(s != null)
                {
                    AddStoryEventToList(s);
                }
                
            }

            GUILayout.Space(10);
            GUILayout.Label("Events:", EditorStyles.boldLabel);
            if(storyEventList.list.Count > 0)
            {
                //wraps storyeventlist in scrollview
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                for (int i = 0; i < storyEventList.list.Count; i++)
                {
                    StoryEvent c = storyEventList.list[i];
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(c.title);
                    if (GUILayout.Button("Edit", GUILayout.ExpandWidth(false))) //opens storyeventeditor
                    {
                        EditStoryEvent(storyEventList.list[i]);
                    }
                    if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false))) //removes reference from list
                    {
                        RemoveStoryEvent(i);
                    }
                    GUILayout.Space(10);
                    //allow developer to swap positions in list
                    string eventPosition = EditorGUILayout.TextField("Position: ", i.ToString(), GUILayout.ExpandWidth(false));
                    int eventPositionInt;
                    if (Int32.TryParse(eventPosition, out eventPositionInt))
                    {
                        if (eventPositionInt != i)
                        {
                            storyEventList.Swap(i, eventPositionInt);
                            EditorUtility.SetDirty(storyEventList);
                            GUI.FocusControl(null);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            else
            {
                GUILayout.Label("No Story Events");
            }
            
        }

        GUILayout.EndScrollView();

    }

    void CreateEventStoryList(string name)
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.

        storyEventList = CreateStoryEvent.CreateStoryEventList(name);
        if (storyEventList)
        {
            storyEventList.list = new List<StoryEvent>();
            string relPath = AssetDatabase.GetAssetPath(storyEventList);
            EditorPrefs.SetString("StoryPath", relPath);
        }
    }

    void OpenStoryEventList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select StoryEventList", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            storyEventList = AssetDatabase.LoadAssetAtPath(relPath, typeof(StoryEventList)) as StoryEventList;
            if (storyEventList.list == null) storyEventList.list = new List<StoryEvent>();
            if (storyEventList)
            {
                EditorPrefs.SetString("StoryPath", relPath);
                folderPath = relPath.Substring(0, relPath.LastIndexOf("/"));

            }
        }
    }
    //
    StoryEvent OpenStoryEvent()
    {
        string absPath = EditorUtility.OpenFilePanel("Select StoryEvent", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            StoryEvent s = AssetDatabase.LoadAssetAtPath(relPath, typeof(StoryEvent)) as StoryEvent;
            if (s)
            {
                return s;
            }
        }
        return null;
    }

    //creates new story event, adds to current storyeventlist
    StoryEvent CreateNewStoryEvent()
    {
        StoryEvent s = CreateStoryEvent.CreateStoryEventAsset(storyEventTitle, folderPath);
        s.Init(storyEventTitle);
        AddStoryEventToList(s);

        //add requirements and dependencies to newly created StoryEvent
        var req = ScriptableObject.CreateInstance<RequirementDict>();
        var dep = ScriptableObject.CreateInstance<Dependencies>();
        s.requirements = req;
        s.dependencies = dep;
        AssetDatabase.AddObjectToAsset(req, s);
        AssetDatabase.AddObjectToAsset(dep, s);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(s)); //needed after using AddObjectToAsset
        return s;
    }

    void AddStoryEventToList(StoryEvent s)
    {
        storyEventList.Add(s);
        EditorUtility.SetDirty(storyEventList);
    }

    void RemoveStoryEvent(int i)
    {
        storyEventList.RemoveAt(i);
    }

    void EditStoryEvent(StoryEvent s)
    {
        StoryEventEditor.Init(s);
    }
}

