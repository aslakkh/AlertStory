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
    private ChoiceList choiceList;
    private RequirementsList requirementList;
    private int _reqIndex = 0;
    private int _depIndex = 0;
    private List<string> requiremetListString = new List<string>();
    private List<string> storyEventListString = new List<string>();
    private string choicePath;
    private string requirementPath;
    private string storyEventPath;

    //editor values
    string storyTitle;
    string storyText;
    Vector2 mainScrollPosition;

    //style variables for listItems
    GUIStyle listItemStyle = new GUIStyle();
    Color[] colors = new Color[] { Color.white, Color.grey };

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
        w.SetEventInfoValues();

    }


    //Update on change.
    private void OnFocus() {

        GUI.FocusControl(null);
    }

    void OnGUI() {
        //entire window should be scrollable
        mainScrollPosition = GUILayout.BeginScrollView(mainScrollPosition);

        if (storyEvent == null)
        {
            if (GUILayout.Button("Open", GUILayout.ExpandWidth(false)))
            {
                OpenStoryEvent();
            }
        }

        else
        {
            GUILayout.Label("Info", EditorStyles.boldLabel);
            storyTitle = EditorGUILayout.TextField("Story title", storyTitle);

            GUILayout.Space(10);

            GUILayout.Label("Story Text");
            storyText = EditorGUILayout.TextArea(storyText, GUILayout.Height(50));

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Info", GUILayout.ExpandWidth(false)))
            {
                SaveInfoValues();
            }
            if (GUILayout.Button("Revert Info", GUILayout.ExpandWidth(false)))
            {
                SetEventInfoValues();
            }
            GUILayout.EndHorizontal();
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
                int i = 0;
                foreach (Choice choice in storyEvent.choices.ToArray())
                {
                    i++;
                    listItemStyle.normal.background = EditorHelperFunctions.MakeTex(1, 1, colors[i % 2]); //used to alternate background colors
                    GUILayout.BeginVertical(listItemStyle);
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
                    if (choiceDescription != choice.choiceDescription)
                    {
                        choice.choiceDescription = choiceDescription;
                        EditorUtility.SetDirty(storyEvent);
                    }

                    GUILayout.Space(10);

                    //edit scores
                    if (choice.scores == null || choice.scores.Count == 0)
                    {
                        GUILayout.Label("No scores attached to this choice");
                    }
                    else
                    {
                        GUILayout.Label("Scores: ", EditorStyles.boldLabel);
                        if(requirementList == null)
                        {
                            GUILayout.Label("Open a requirementlist to be able to add requirements to scores.", EditorStyles.helpBox);
                        }
                        
                        //list all scores
                        foreach(Score score in choice.scores.ToArray())
                        {
                            GUILayout.BeginHorizontal();
                            if(!string.IsNullOrEmpty(score.requirementName)) //allow setting of requirement to be changed
                            {
                                GUILayout.Label("If " + score.requirementName + " is ");
                                var setting = (Setting)EditorGUILayout.EnumPopup(score.setting);

                                //Update only on value change check
                                if (setting != score.setting)
                                {
                                    score.SetSetting(setting);
                                    EditorUtility.SetDirty(storyEvent.requirements);
                                }
                            }
                            else if(requirementList != null) //allow adding of requirement
                            {
                                // Create popup and add to list
                                _reqIndex = EditorGUILayout.Popup("Add Requirement?", _reqIndex,
                                    requiremetListString.ToArray());
                                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                                {
                                    score.SetRequirementName(requirementList.list[_reqIndex].requirementName);
                                    score.SetSetting(Setting.Public);
                                    EditorUtility.SetDirty(storyEvent.requirements);
                                }
                            }

                            int value = EditorGUILayout.IntField("Score value: ", score.value);
                            if(value != score.value)
                            {
                                score.SetValue(value);
                                EditorUtility.SetDirty(storyEvent);
                            }

                            //Delete Button for score
                            if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                            {
                                choice.scores.Remove(score);
                                EditorUtility.SetDirty(storyEvent);
                            }

                            GUILayout.EndHorizontal();
                        }
                    }

                    //add new score element to choice.scores
                    if (GUILayout.Button("Add Score", GUILayout.ExpandWidth(false)))
                    {
                        choice.AddNewScore();
                        EditorUtility.SetDirty(storyEvent);
                    }


                    //close friend element vertical
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
                        EditorUtility.SetDirty(storyEvent.requirements);
                    }
                    //Delete Button for choice, only in relevant story.
                    if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                    {
                        storyEvent.requirements.Remove(item.Key);
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
                    storyEvent.requirements.Add(requirementList.list[_reqIndex].requirementName, Setting.Public);
                    EditorUtility.SetDirty(storyEvent.requirements);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(3);
                if (GUILayout.Button("Select other requirementlist", GUILayout.ExpandWidth(false)))
                {
                    OpenRequirementList();
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
                    GUILayout.BeginHorizontal();

                    //Dispalys name as Static and boolean as interchangable
                    GUILayout.Label(item.Key.title);
                    var firedRequired = EditorGUILayout.Toggle("Must be fired", item.Value.fired);
                    if (item.Key.choices.Count == 2)
                    {
                        var choiceARequired = EditorGUILayout.Toggle("Require Choice: " + item.Key.choices[0].choiceDescription, item.Value.choiceA);
                        var choiceBRequired = EditorGUILayout.Toggle("Require Choice: " + item.Key.choices[1].choiceDescription, item.Value.choiceB);

                        if(choiceARequired != item.Value.choiceA)
                        {
                            storyEvent.dependencies.UpdateValue(item.Key, new StoryDependencyBool(item.Value.fired, choiceARequired, item.Value.choiceB));
                            EditorUtility.SetDirty(storyEvent.dependencies);
                        }
                        if(choiceBRequired != item.Value.choiceB)
                        {
                            storyEvent.dependencies.UpdateValue(item.Key, new StoryDependencyBool(item.Value.fired, item.Value.choiceA, choiceBRequired));
                            EditorUtility.SetDirty(storyEvent.dependencies);
                        }
                    }
                    //Update only on value change check
                    if (firedRequired != item.Value.fired)
                    {
                        storyEvent.dependencies.UpdateValue(item.Key, new StoryDependencyBool(firedRequired, item.Value.choiceA, item.Value.choiceB));
                        EditorUtility.SetDirty(storyEvent.dependencies);
                    }
                    //Delete Button for choice, only in relevant story.
                    if (GUILayout.Button("Delete", GUILayout.ExpandWidth(false)))
                    {
                        storyEvent.dependencies.Remove(item.Key);
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
                    //add dependency (with fired-requirement set to true, choiceA requirement set to false);
                    storyEvent.dependencies.Add(DependencyList.list[_depIndex], new StoryDependencyBool(true, false, false));
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

        GUILayout.EndScrollView();
    }

    void OpenStoryEvent()
    {
        string absPath = EditorUtility.OpenFilePanel("Select StoryEvent", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            storyEvent = AssetDatabase.LoadAssetAtPath(relPath, typeof(StoryEvent)) as StoryEvent;
            if (storyEvent)
            {
                EditorPrefs.SetString("ObjectPath", relPath);

                //init editable strings
                SetEventInfoValues();
            }
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

    void SetEventInfoValues()
    {
        if(storyEvent != null)
        {
            storyTitle = storyEvent.title;
            storyText = storyEvent.text;
            EditorUtility.SetDirty(storyEvent);
        }
        
    }

    public void SaveInfoValues()
    {
        if(storyEvent != null)
        {
            storyEvent.title = storyTitle;
            storyEvent.text = storyText;
        }
    }
}