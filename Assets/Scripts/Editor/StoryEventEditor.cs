using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using Settings;
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
    private List<string> choiceListString = new List<string>();
    private List<string> requiremetListString = new List<string>();
    private List<string> storyEventListString = new List<string>();
    private string choicePath;
    private string requiremetPath;
    private string storyEventPath;

    [MenuItem("Window/Event/StoryEditor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(StoryEventEditor));
    }


    //Update on change.
    private void OnFocus() {
        if (!string.IsNullOrEmpty(choicePath))
        {
            SetChoiceList(choicePath);
        }
        if (!string.IsNullOrEmpty(requiremetPath)){
            SetRequirementsList(requiremetPath);
        }

        if (!string.IsNullOrEmpty(storyEventPath))
        {
            SetDepenedencyList(storyEventPath);
        }
        
    }

    void OnEnable() {
        if (EditorPrefs.HasKey("StoryPath")){
            string objectPath = EditorPrefs.GetString("StoryPath");
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
                
                //Open finder if no choice assets are loaded
                if (choiceList == null) {
                    if (GUILayout.Button("Open Choices", GUILayout.ExpandWidth(false))) {
                        OpenChoiceList();
                    };
                } else {
                    GUILayout.BeginHorizontal();
                    _choiceIndex = EditorGUILayout.Popup("Add Choices", _choiceIndex, choiceListString.ToArray());
                    if (GUILayout.Button("Add", GUILayout.ExpandWidth(false))) {
                        storyEventList.list[viewIndex - 1].choices.Add(choiceList[_choiceIndex]);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(3);
                    if(GUILayout.Button("Select other choicelist", GUILayout.ExpandWidth(false))) {
                        OpenChoiceList();
                    }
                }
                
                GUILayout.Space(20);
                
                //Requirements list
                //Does it exsist??
                if (storyEventList.list[viewIndex - 1].requirements == null || storyEventList.list[viewIndex - 1].requirements.requirementDictionary == null) {
                    GUILayout.Label(" No requirements.");
                    GUILayout.Space(2);
                }
                else {
                    if(storyEventList.list[viewIndex - 1].requirements.requirementDictionary.Count > 0)
                    {
                        //Debug.Log(storyEventList.list[viewIndex - 1].requirements.requirementDictionary.ElementAt(0));
                    }
                    //Loops trough each requirement in list
                    for (int i = 0; i < storyEventList.list[viewIndex - 1].requirements.requirementDictionary
                        .Count; i++) {
                        var item = storyEventList.list[viewIndex - 1].requirements.requirementDictionary.ElementAt(i);
                        GUILayout.BeginHorizontal();
                        
                        //Dispalys name as Static and boolean as interchangable

                        EditorGUILayout.TextArea(item.Key.ToString());
                        var value =(Setting)EditorGUILayout.EnumPopup("Required?", item.Value);

                        //Update only on value change check
                        if (value != item.Value) {
                            storyEventList.list[viewIndex - 1].requirements.UpdateValue(item.Key, value);
                            EditorUtility.SetDirty(storyEventList);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1].requirements);
                        }
                        //Delete Button for choice, only in relevant story.
                        if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false))) {
                            Debug.Log(item.Key);
                            Debug.Log(storyEventList.list[viewIndex - 1].requirements.requirementDictionary.ElementAt(0));
                            storyEventList.list[viewIndex - 1].requirements.Remove(item.Key);
                            EditorUtility.SetDirty(storyEventList);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1].requirements);
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.Space(2);
                    }
                }
                
                //Open finder if no requirements asset is loaded
                if (requirementList == null) {
                    if (GUILayout.Button("Open requirement", GUILayout.ExpandWidth(false))) {
                        OpenRequirementList();
                    }
                } else {
                    GUILayout.BeginHorizontal();
                    // Create popup and add to list
                    _reqIndex = EditorGUILayout.Popup("Add Requirement", _reqIndex,
                        requiremetListString.ToArray());
                    if (GUILayout.Button("Add", GUILayout.ExpandWidth(false))) {
                        Debug.Log(requirementList.list[_reqIndex]);
                        storyEventList.list[viewIndex - 1].requirements.Add(requirementList.list[_reqIndex].requirementName, Setting.Public);
                        EditorUtility.SetDirty(storyEventList);
                        EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                        EditorUtility.SetDirty(storyEventList.list[viewIndex - 1].requirements);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(3);
                    if(GUILayout.Button("Select other requirementlist", GUILayout.ExpandWidth(false))) {
                        OpenRequirementList();
                    } else {
                        // Create popup and add to list
                        //_reqIndex = EditorGUILayout.Popup("Add Requirement", _reqIndex,
                        //    requiremetListString.ToArray());
                        //storyEventList.list[viewIndex - 1].requirements.Add(requirementList.list[_reqIndex], Setting.Public);
                    }
                }
                
                GUILayout.Space(20);
                //Dependencies list
                //Does it exsist??
                if (storyEventList.list[viewIndex - 1].dependencies == null || storyEventList.list[viewIndex - 1].dependencies.dependenciesDict == null) {
                    GUILayout.Label(" No dependencies.");
                    GUILayout.Space(2);
                }
                else {
                    //Loops trough each Dependencies in list
                    for (int i = 0; i < storyEventList.list[viewIndex - 1].dependencies.dependenciesDict.Count; i++) {
                        var item = storyEventList.list[viewIndex - 1].dependencies.dependenciesDict.ElementAt(i);
                        //Debug.Log(storyEventList.list[viewIndex - 1].dependencies.dependenciesDict.ContainsKey(item.Key));
                        GUILayout.BeginHorizontal();
                        
                        //Dispalys name as Static and boolean as interchangable
                        EditorGUILayout.TextArea(item.Key.title);
                        var value = EditorGUILayout.Toggle("Checked for must", item.Value);
                        //Update only on value change check
                        if (value != item.Value) {
                            Debug.Log(storyEventList.list[viewIndex - 1].dependencies.dependenciesDict.ElementAt(0).GetHashCode() == item.GetHashCode());
                            storyEventList.list[viewIndex - 1].dependencies.UpdateValue(item.Key, value);
                            EditorUtility.SetDirty(storyEventList);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1].dependencies);

                        }
                        //Delete Button for choice, only in relevant story.
                        if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false))) {
                            storyEventList.list[viewIndex - 1].dependencies.Remove(item.Key);
                            EditorUtility.SetDirty(storyEventList);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                            EditorUtility.SetDirty(storyEventList.list[viewIndex - 1].dependencies);
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
                        Debug.Log(storyEventList.list[viewIndex - 1].dependencies.dependenciesDict.Count);
                        storyEventList.list[viewIndex - 1].dependencies.Add(DependencyList.list[_depIndex], true);
                        EditorUtility.SetDirty(storyEventList);
                        EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                        EditorUtility.SetDirty(storyEventList.list[viewIndex - 1].dependencies);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(3);
                    if(GUILayout.Button("Select other Dependencylist", GUILayout.ExpandWidth(false))) {
                        OpenDependencyList();
                    }
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
            AssetDatabase.SaveAssets();
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
            EditorPrefs.SetString("StoryPath", relPath);
        }
    }

    void OpenItemList(){
        string absPath = EditorUtility.OpenFilePanel("Select StoryEventList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            storyEventList = AssetDatabase.LoadAssetAtPath(relPath, typeof(StoryEventList)) as StoryEventList;
            if (storyEventList.list == null)storyEventList.list = new List<StoryEvent>();
            if (storyEventList){
                EditorPrefs.SetString("StoryPath", relPath);
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
        if(requiremetListString != null)
        {
            requiremetListString.Clear();
        }
        
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
    }
    
    void AddItem() {
        StoryEvent newItem = ScriptableObject.CreateInstance<StoryEvent>();
        //Debug.Log(storyEventList.list);
        storyEventList.list.Add(newItem);
        //newItem.Init();
        AssetDatabase.AddObjectToAsset(newItem, storyEventList);

        // Reimport the asset after adding an object.
        // Otherwise the change only shows up when saving the project
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(storyEventList));


        newItem.Init();


        viewIndex = storyEventList.list.Count;
    }

    void DeleteItem(int index) { 
        storyEventList.list.RemoveAt(index);
    }
}