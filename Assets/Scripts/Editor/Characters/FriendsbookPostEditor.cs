using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FriendsbookPostEditor : EditorWindow {

    private FriendsbookPost post;
    private Character character;

    public string postContent;
    public int postDay;
    public int postMonth;
    public int postYear;
    public int postHour;
    public int postMinute;
    public FriendsbookProfile postFrom;
    public bool newPost;

    public bool displayError;

    public string charactersSearchString = "";
    Vector2 scrollPosition;

    //Date dropdown variables
    private int[] days = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
    private string[] dayStrings = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
    private int[] months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    private string[] monthStrings = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
    private int[] years = new int[] { 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018 };
    private string[] yearStrings = new string[] { "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018 " };
    private int[] hours = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
    private string[] hourStrings = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };

    //style variables for listItems
    GUIStyle listItemStyle = new GUIStyle();
    Color[] colors = new Color[] { Color.white, Color.grey };

    public static void Init(Character c) //init window with new post
    {
        var w = EditorWindow.GetWindow<FriendsbookPostEditor>(typeof(CharacterEditor)); //opens post editor, tries to dock next to existing charactereditor
        w.post = new FriendsbookPost();
        w.character = c;
        w.newPost = true;
        w.displayError = false;
        w.postContent = "";
        w.postDay = w.days[0];
        w.postMonth = w.months[0];
        w.postYear = w.years[0];
        w.postHour = w.hours[0];
        w.postMinute = 0;
    }

    public static void Init(FriendsbookPost post) //init window to edit existing post
    {
        var w = EditorWindow.GetWindow<FriendsbookPostEditor>(typeof(CharacterEditor)); //opens post editor, tries to dock next to existing charactereditor
        w.post = post;
        w.character = post.to.character;
        w.SetEditableValues();
        w.newPost = false;
        w.displayError = false;
    }

    private void OnFocus()
    {
        GUI.FocusControl(null);
    }

    private void OnGUI()
    {
        GUILayout.Label("Post Content:");
        postContent = EditorGUILayout.TextArea(postContent, GUILayout.Height(50));
        //input date
        GUILayout.BeginVertical();
        postDay = EditorGUILayout.IntPopup("Day:", postDay, dayStrings, days);
        postMonth = EditorGUILayout.IntPopup("Month:", postMonth, monthStrings, months);
        postYear = EditorGUILayout.IntPopup("Year:", postYear, yearStrings, years);
        postHour = EditorGUILayout.IntPopup("Hour:", postHour, hourStrings, hours);
        postMinute = EditorGUILayout.IntField("Minute:", postMinute);
        //simple validation
        if (postMinute > 59) { postMinute = 59; }
        if (postMinute < 0) { postMinute = 0; }
        GUILayout.EndVertical();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("From:", EditorStyles.boldLabel);
        if (postFrom != null)
        {
            GUILayout.Label(postFrom.character.fullName);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.Label("Who is the post from? Select the current character if the post has no sender (status updates)", EditorStyles.helpBox);
        //searchable list of characters to set as from
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Label("Search: ");
        charactersSearchString = EditorGUILayout.TextField(charactersSearchString);
        GUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Height(200));

        List<Character> filteredCharacters = character.characterList.list.FindAll(c => c.fullName.ToLower().Contains(charactersSearchString.ToLower()) && (c.friendsbookProfile != null));
        for (int i = 0; i < filteredCharacters.Count; i++)
        {
            Character c = filteredCharacters[i];
            listItemStyle.normal.background = EditorHelperFunctions.MakeTex(1, 1, colors[i % 2]); //used to alternate background colors
            GUILayout.BeginHorizontal(listItemStyle);
            GUILayout.Label(c.fullName);
            if (GUILayout.Button("Select", GUILayout.ExpandWidth(false)))
            {
                postFrom = c.friendsbookProfile;
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", GUILayout.ExpandWidth(false)))
        {
            SavePost();
        }

        if (!newPost)
        {
            if (GUILayout.Button("Revert", GUILayout.ExpandWidth(false)))
            {
                SetEditableValues();
            }
        }
        

        GUILayout.EndHorizontal();

        if (displayError)
        {
            GUILayout.Label("Missing input...", EditorStyles.boldLabel);
        }
    }


    //set editor values to current post values
    public void SetEditableValues()
    {
        postContent = post.content;
        postDay = post.date.day;
        postMonth = post.date.month;
        postYear = post.date.year;
        postHour = post.date.hour;
        postMinute = post.date.minute;
        postFrom = post.from;
    }

    public void SavePost()
    {
        

        //simple validation
        if (string.IsNullOrEmpty(postContent) || postFrom == null)
        {
            displayError = true;
        }
        else
        {
            displayError = false;
            post.content = postContent;
            post.date = new Date(postYear, postMonth, postDay, postHour, postMinute);
            post.to = character.friendsbookProfile;
            post.from = postFrom;

            if (newPost) //add to receiving characters postlist
            {
                post.to.posts.Add(post);
                newPost = false;
            }
            EditorUtility.SetDirty(character.friendsbookProfile);
            EditorUtility.SetDirty(character.friendsbookProfile.posts);
        }


    }

}
