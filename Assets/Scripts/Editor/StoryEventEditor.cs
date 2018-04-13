using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class StoryEventEditor : EditorWindow
{

    public StoryEventList storyEventList;
    public StoryEventList DependencyList;
    private int viewIndex = 1;
    private ChoiceList choiceList;
    private RequirementsList requirementList;
    private int _choiceIndex = 0;
    private int _reqIndex = 0;
    private int _depIndex = 0;
    private List<string> choiceListString;
    private List<string> requiremetListString;
    private List<string> storyEventListString;
    private string choicePath = "Assets/Resources/ScriptableObjects/Events/EventList.asset";
    private string requiremetPath = "Assets/Resources/ScriptableObjects/Events/RequirementList.asset";
    private string storyEventPath;
    

    [MenuItem("Window/Event/StoryEditor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(StoryEventEditor));
    }
    //Update on change.
    private void OnFocus() {
        SetChoiceList(choicePath);
        SetRequirementsList(requiremetPath);
        SetDepenedencyList(storyEventPath);
    }

    void OnEnable() {
        if (EditorPrefs.HasKey("ObjectPath")){
            string objectPath = EditorPrefs.GetString("ObjectPath");
            storyEventPath = objectPath;
            storyEventList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(StoryEventList)) as StoryEventList;
        }
       
    }

    void OnGUI() {
       
        GUILayout.BeginHorizontal();
        GUILayout.Label("Choice Editor", EditorStyles.boldLabel);
        if (storyEventList != null) {
            if (GUILayout.Button("Show Item List")) {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = storyEventList;
            }
        }
        if (GUILayout.Button("Open Item List")) {
            OpenItemList();
        }
        GUILayout.EndHorizontal();

        if (storyEventList == null) {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New StoryEvent List", GUILayout.ExpandWidth(false))) {
                CreateEventStoryList();
            }
            if (GUILayout.Button("Open Existing StoryEvent List", GUILayout.ExpandWidth(false))) {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (storyEventList != null) {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) {
                if (viewIndex < storyEventList.list.Count) {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (storyEventList.list != null && storyEventList.list.Count > 0) {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Story Event", viewIndex, GUILayout.ExpandWidth(false)), 1, storyEventList.list.Count);
                //Mathf.Clamp (viewIndex, 1, itemTypeList.list.Count);
                EditorGUILayout.LabelField("of   " + storyEventList.list.Count.ToString() + "  events", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                storyEventList.list[viewIndex - 1].title = EditorGUILayout.TextField("Story title", storyEventList.list[viewIndex - 1].title);

                GUILayout.Space(10);
                
                storyEventList.list[viewIndex - 1].text = EditorGUILayout.TextField("Story Text", storyEventList.list[viewIndex - 1].text);

                GUILayout.Space(10);
                
                //Choicelist
                //Does it exsist??
                if (storyEventList.list[viewIndex - 1].choices == null) {
                    GUILayout.Label(" No choices yo.");
                    GUILayout.Space(2);
                } else {
                    //Loop trough each Choice
                    foreach (Choice choice in storyEventList.list[viewIndex - 1].choices.ToArray()) {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(choice.choiceDescription);
                        //Delete button for choice
                        if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false))) {
                            storyEventList.list[viewIndex - 1].choices.Remove(choice);
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.Space(2);
                    }
                }
                //Add Button for Choice
                if (GUILayout.Button("Add Choices", GUILayout.ExpandWidth(false))) {
                    //Open finder if no choice assets are loaded
                    if (choiceList == null) {
                        OpenChoiceList();
                    } else {
                        _choiceIndex = EditorGUILayout.Popup("Add Choices", _choiceIndex, choiceListString.ToArray());
                        storyEventList.list[viewIndex - 1].choices.Add(choiceList[_choiceIndex]);
                    }
                }
                GUILayout.Space(5);
                
                //Requirements list
                //Does it exsist??
                if (storyEventList.list[viewIndex - 1].requirements.requirementDictionary == null) {
                    GUILayout.Label(" No requirements.");
                    GUILayout.Space(2);
                }
                else {
                    //Loops trough each requirement in list
                    foreach (var item in storyEventList.list[viewIndex - 1].requirements.requirementDictionary
                        .ToArray()) {
                        GUILayout.BeginHorizontal();
                        
                        //Dispalys name as Static and boolean as interchangable
                        EditorGUILayout.TextArea(item.Key.ToString());
                        var value = EditorGUILayout.Toggle("Required?", item.Value);
                        //Update only on value change check
                        if (value != item.Value) {
                            storyEventList.list[viewIndex - 1].requirements.Update(item.Key, value);
                        }
                        //Delete Button for choice, only in relevant story.
                        if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false))) {
                            storyEventList.list[viewIndex - 1].requirements.Remove(item.Key);
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.Space(2);
                    }
                }
                //Add button for Requirements
                if (GUILayout.Button("Add Requirement", GUILayout.ExpandWidth(false))) {
                    //Open finder if no requirements asset is loaded
                    if (requirementList == null) {
                        OpenRequirementList();
                    } else {
                        // Create popup and add to list
                        _reqIndex = EditorGUILayout.Popup("Add Requirement", _reqIndex,
                            requiremetListString.ToArray());
                        storyEventList.list[viewIndex - 1].requirements.Add(requirementList.list[_reqIndex], false);
                    }
                }
                GUILayout.Space(5);
                //Dependencies list
                //Does it exsist??
                if (storyEventList.list[viewIndex - 1].dependencies.dependenciesDict == null) {
                    GUILayout.Label(" No dependencies.");
                    GUILayout.Space(2);
                }
                else {
                    //Loops trough each Dependencies in list
                    foreach (var item in storyEventList.list[viewIndex - 1].dependencies.dependenciesDict
                        .ToArray()) {
                        GUILayout.BeginHorizontal();
                        
                        //Dispalys name as Static and boolean as interchangable
                        EditorGUILayout.TextArea(item.Key.ToString());
                        var value = EditorGUILayout.Toggle("Checked for must, and empty for not", item.Value);
                        //Update only on value change check
                        if (value != item.Value) {
                            storyEventList.list[viewIndex - 1].dependencies.Update(item.Key, value);
                        }
                        //Delete Button for choice, only in relevant story.
                        if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false))) {
                            storyEventList.list[viewIndex - 1].dependencies.Remove(item.Key);
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.Space(2);
                    }
                }
                //Add button for Dependencies
                if (DependencyList == null) {
                    if (GUILayout.Button("Open Dependency List", GUILayout.ExpandWidth(false))) {
                        OpenDependencyList();
                    }
                }
                else {
                    GUILayout.BeginHorizontal();
                    _depIndex = EditorGUILayout.Popup("Add Dependencies", _depIndex,
                        storyEventListString.ToArray());
                    if (GUILayout.Button("Add Dependencies", GUILayout.ExpandWidth(false))) {
                        // Create popup and add to list
                        storyEventList.list[viewIndex - 1].dependencies.Add(DependencyList.list[_depIndex], true);
                    }
                    GUILayout.EndHorizontal();
                }
                
                GUILayout.Space(5);
                
                
            } else {
                // If no StoryEvent is in list
                GUILayout.Label("This StoryEventList is Empty.");
            }
        }
        //Update changes on new changes to editor.
        if (GUI.changed) {
            EditorUtility.SetDirty(storyEventList);
        }
    }
    
    void CreateEventStoryList() {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        // TODO Set new protocol for defining name
        
        viewIndex = 1;
        storyEventList = CreateStoryEvent.Create();
        if (choiceList){
            choiceList.list = new List<Choice>();
            string relPath = AssetDatabase.GetAssetPath(choiceList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList(){
        string absPath = EditorUtility.OpenFilePanel("Select StoryEventList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            storyEventList = AssetDatabase.LoadAssetAtPath(relPath, typeof(StoryEventList)) as StoryEventList;
            if (storyEventList.list == null)storyEventList.list = new List<StoryEvent>();
            if (storyEventList){
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void OpenChoiceList(){
        string absPath = EditorUtility.OpenFilePanel("Select ChoiceList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            choicePath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SetChoiceList(choicePath);
        }
    } 
    
    void OpenRequirementList(){
        string absPath = EditorUtility.OpenFilePanel("Select RequirementList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            requiremetPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SetRequirementsList(requiremetPath);
        }
    }
    
    void OpenDependencyList(){
        string absPath = EditorUtility.OpenFilePanel("Select DependencyList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            storyEventPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SetDepenedencyList(storyEventPath);
        }
    }
    
    //Helper function to set choicelist after update.
    void SetChoiceList(string path) {
        choiceList = AssetDatabase.LoadAssetAtPath(path, typeof(ChoiceList)) as ChoiceList;
        choiceListString.Clear();
        if (choiceList == null) return;
        foreach (Choice item in choiceList.list) {
            choiceListString.Add(item.choiceDescription);
        }
    }
    
    //Helper function to set requiremenlist after update.
    void SetRequirementsList(string path) {
        requirementList = AssetDatabase.LoadAssetAtPath(path, typeof(RequirementsList)) as RequirementsList;
        requiremetListString.Clear();
        if (requirementList == null) return;
        foreach (Requirement item in requirementList.list) {
            requiremetListString.Add(item.requirementName);
        }
    }
    
    //Helper function to set dependency after update.
    void SetDepenedencyList(string path) {
        DependencyList = AssetDatabase.LoadAssetAtPath(path, typeof(StoryEventList)) as StoryEventList;
        storyEventListString.Clear();
        if (DependencyList == null) return;
        foreach (StoryEvent item in DependencyList.list) {
            storyEventListString.Add(item.title);
        }
        Debug.Log(storyEventListString.ToArray());
    }
    
    void AddItem() {
        StoryEvent newItem = new StoryEvent {title = "New story event"};
        storyEventList.list.Add(newItem);
        viewIndex = storyEventList.list.Count;
    }

    void DeleteItem(int index) { 
        storyEventList.list.RemoveAt(index);
    }
}