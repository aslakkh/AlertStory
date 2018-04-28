using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using Settings;
public class StoryEventEditor : EditorWindow
{

    public StoryEventList storyEventList;
    public StoryEventList DependencyList;
    private StoryEvent storyEvent; //storyEvent being edited
    private string folderPath; //path of storyEvent being edited
    private ChoiceList choiceList;
    private RequirementsList requirementList;
    private int _choiceIndex = 0;
    private int _reqIndex = 0;
    private int _depIndex = 0;
    private List<string> choiceListString = new List<string>();
    private List<string> requiremetListString = new List<string>();
    private List<string> storyEventListString = new List<string>();
    private string choicePath;
    private string requirementPath;
    private string storyEventPath;

    //editor values
    string storyTitle;
    string storyText;

    [MenuItem("Window/Event/StoryEventEditor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(StoryEventEditor));
    }

    //call this to instantiate editor window
    public static void Init(StoryEvent s)
    {
        //getwindow, attempt to dock next to existing CharacterListEditor
        StoryEventEditor w = EditorWindow.GetWindow<StoryEventEditor>(typeof(StoryEventListEditor));
        w.storyEvent = s;

        //set folderPath
        string folderPath = AssetDatabase.GetAssetPath(s); //gives relative path of asset
        folderPath = folderPath.Substring(0, folderPath.LastIndexOf("/")); //strip to give path to asset folder
        w.folderPath = folderPath;

    }


    //Update on change.
    private void OnFocus() {

        GUI.FocusControl(null);
    }

    void OnGUI() {

        if(storyEvent == null)
        {
            //do something
        }

        else
        {
            GUILayout.Label("Info", EditorStyles.boldLabel);
            storyTitle = EditorGUILayout.TextField("Story title", storyTitle);

            GUILayout.Space(10);

            storyText = EditorGUILayout.TextField("Story Text", storyText);

            GUILayout.Space(10);

            GUILayout.Label("Choices", EditorStyles.boldLabel);
            //Choicelist
            //Does it exsist??
            if (storyEvent.choices == null || storyEvent.choices.Count == 0)
            {
                GUILayout.Label("Warning: No choices. Every StoryEvent needs minimum 1 choice.", EditorStyles.helpBox);
                GUILayout.Space(2);

                //adds new choice to storyEvent
                if (GUILayout.Button("Add New Choice", GUILayout.ExpandWidth(false)))
                {
                    storyEvent.choices.Add(new Choice());
                }
            }
            else
            {
                //Loop trough each Choice
                foreach (Choice choice in storyEvent.choices.ToArray())
                {
                    GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(choice.choiceDescription, EditorStyles.largeLabel);
                    //Delete button for choice
                    if (GUILayout.Button("Delete Choice", GUILayout.ExpandWidth(false)))
                    {
                        storyEvent.choices.Remove(choice);
                        EditorUtility.SetDirty(storyEvent);
                    }
                    GUILayout.EndHorizontal();

                    //edit choiceDescription
                    string choiceDescription = EditorGUILayout.TextField("Choice Description", choice.choiceDescription);
                    int affectScorePrivate = EditorGUILayout.IntField("Affect Score Private", choice.affectScorePrivate);
                    int affectScoreFriends = EditorGUILayout.IntField("Affect Score Friends", choice.affectScoreFriends);
                    int affectScorePublic = EditorGUILayout.IntField("Affect Score Public", choice.affectScorePublic);
                    if (choiceDescription != choice.choiceDescription)
                    {
                        choice.choiceDescription = choiceDescription;
                        EditorUtility.SetDirty(storyEvent);
                    }
                    if(affectScorePrivate != choice.affectScorePrivate)
                    {
                        choice.affectScorePrivate = affectScorePrivate;
                        EditorUtility.SetDirty(storyEvent);
                    }
                    if(affectScoreFriends != choice.affectScoreFriends)
                    {
                        choice.affectScoreFriends = affectScoreFriends;
                        EditorUtility.SetDirty(storyEvent);
                    }
                    if(affectScorePublic != choice.affectScorePublic)
                    {
                        choice.affectScorePublic = affectScorePublic;
                        EditorUtility.SetDirty(storyEvent);
                    }

                    GUILayout.EndVertical();
                    GUILayout.Space(10);
                }

                if(storyEvent.choices.Count < 2) //allow at most 2 choices
                {
                    //adds new choice to storyEvent
                    if (GUILayout.Button("Add New Choice", GUILayout.ExpandWidth(false)))
                    {
                        storyEvent.choices.Add(new Choice());
                    }
                }
                
            }

            GUILayout.Space(20);

            GUILayout.Label("Requirements", EditorStyles.boldLabel);
            //Requirements list
            //Does it exsist??
            if (storyEvent.requirements == null || storyEvent.requirements.requirementDictionary == null)
            {
                GUILayout.Label(" No requirements.");
                GUILayout.Space(2);
            }
            else
            {
                //Loops trough each requirement in list
                for (int i = 0; i < storyEvent.requirements.requirementDictionary.Count; i++)
                {
                    var item = storyEvent.requirements.requirementDictionary.ElementAt(i);
                    GUILayout.BeginHorizontal();

                    //Dispalys name as Static and boolean as interchangable
                    GUILayout.Label(item.Key);
                    var value = (Setting)EditorGUILayout.EnumPopup("Setting", item.Value);

                    //Update only on value change check
                    if (value != item.Value)
                    {
                        storyEvent.requirements.UpdateValue(item.Key, value);
                        //EditorUtility.SetDirty(storyEventList);
                        //EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                        EditorUtility.SetDirty(storyEvent.requirements);
                    }
                    //Delete Button for choice, only in relevant story.
                    if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                    {
                        storyEvent.requirements.Remove(item.Key);
                        //EditorUtility.SetDirty(storyEventList);
                        //EditorUtility.SetDirty(storyEventList.list[viewIndex - 1]);
                        EditorUtility.SetDirty(storyEvent.requirements);
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }
            }

            //Open finder if no requirements asset is loaded
            if (requirementList == null)
            {
                if (GUILayout.Button("Open requirement", GUILayout.ExpandWidth(false)))
                {
                    OpenRequirementList();
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                // Create popup and add to list
                _reqIndex = EditorGUILayout.Popup("Add Requirement", _reqIndex,
                    requiremetListString.ToArray());
                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                {
                    Debug.Log(requirementList.list[_reqIndex]);
                    storyEvent.requirements.Add(requirementList.list[_reqIndex].requirementName, Setting.Public);
                    //EditorUtility.SetDirty(storyEventList);
                    //EditorUtility.SetDirty(storyEvent);
                    EditorUtility.SetDirty(storyEvent.requirements);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(3);
                if (GUILayout.Button("Select other requirementlist", GUILayout.ExpandWidth(false)))
                {
                    OpenRequirementList();
                }
                else
                {
                    // Create popup and add to list
                    //_reqIndex = EditorGUILayout.Popup("Add Requirement", _reqIndex,
                    //    requiremetListString.ToArray());
                    //storyEventList.list[viewIndex - 1].requirements.Add(requirementList.list[_reqIndex], Setting.Public);
                }
            }

            GUILayout.Space(20);

            GUILayout.Label("Dependencies", EditorStyles.boldLabel);
            //Dependencies list
            //Does it exsist??
            if (storyEvent.dependencies == null || storyEvent.dependencies.dependenciesDict == null)
            {
                GUILayout.Label(" No dependencies.");
                GUILayout.Space(2);
            }
            else
            {
                //Loops trough each Dependencies in list
                for (int i = 0; i < storyEvent.dependencies.dependenciesDict.Count; i++)
                {
                    var item = storyEvent.dependencies.dependenciesDict.ElementAt(i);
                    //Debug.Log(storyEventList.list[viewIndex - 1].dependencies.dependenciesDict.ContainsKey(item.Key));
                    GUILayout.BeginHorizontal();

                    //Dispalys name as Static and boolean as interchangable
                    GUILayout.Label(item.Key.title);
                    var value = EditorGUILayout.Toggle("Must be fired", item.Value);
                    //if(item.Key.choices.Count > 1)
                    //{
                    //    var choiceARequired = EditorGUILayout.Toggle("Require Choice: " + item.Key.choices[0].choiceDescription, item.Value);
                    //    if(item.Key.choices.Count == 2)
                    //    {
                    //        var choiceBRequired = EditorGUILayout.Toggle("Require Choice: " + item.Key.choices[1].choiceDescription, item.Value);
                    //    }
                    //}
                    //Update only on value change check
                    if (value != item.Value)
                    {
                        storyEvent.dependencies.UpdateValue(item.Key, value);
                        //EditorUtility.SetDirty(storyEventList);
                        //EditorUtility.SetDirty(storyEvent);
                        EditorUtility.SetDirty(storyEvent.dependencies);

                    }
                    //Delete Button for choice, only in relevant story.
                    if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                    {
                        storyEvent.dependencies.Remove(item.Key);
                        //EditorUtility.SetDirty(storyEventList);
                        //EditorUtility.SetDirty(storyEvent);
                        EditorUtility.SetDirty(storyEvent.dependencies);
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }
            }
            //Add button for Dependencies
            if (DependencyList == null)
            {
                if (GUILayout.Button("Open Dependency List", GUILayout.ExpandWidth(false)))
                {
                    OpenDependencyList();
                }
            }
            else
            {
                GUILayout.Label("Use Select Dependency List to select StoryEventList to browse", EditorStyles.helpBox);
                GUILayout.BeginHorizontal();
                _depIndex = EditorGUILayout.Popup("Add Dependencies", _depIndex,
                    storyEventListString.ToArray());
                if (GUILayout.Button("Add Dependencies", GUILayout.ExpandWidth(false)))
                {
                    // Create popup and add to list
                    storyEvent.dependencies.Add(DependencyList.list[_depIndex], true);
                    //EditorUtility.SetDirty(storyEventList);
                    //EditorUtility.SetDirty(storyEvent);
                    EditorUtility.SetDirty(storyEvent.dependencies);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(3);
                if (GUILayout.Button("Select Dependency List", GUILayout.ExpandWidth(false)))
                {
                    OpenDependencyList();
                }
            }

            GUILayout.Space(5);
        }
        

    }
    
    void OpenRequirementList(){
        string absPath = EditorUtility.OpenFilePanel("Select RequirementList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            requirementPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SetRequirementsList(requirementPath);
        }
    }
    
    void OpenDependencyList(){
        string absPath = EditorUtility.OpenFilePanel("Select DependencyList", "", "");
        if (absPath.StartsWith(Application.dataPath)){
            storyEventPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SetDepenedencyList(storyEventPath);
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
    

    void DeleteItem(int index) { 
        storyEventList.list.RemoveAt(index);
    }
}