using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow {

    public Character character;
    public string folderPath;

    //all character attributes 
    public string fullName;
    public string address;
    public string email;
    public string phoneNumber;

    /// SEARCH STRINGS ///
    public string charactersSearchString = "";
    public string currentFriendsSearchString = "";

    /// GUI CONTROL VARIABLES ///

    Vector2 mainScrollPosition;

    //sections
    bool showFriendsbook;

    bool showFriendsbookSettings;
    bool showFriendsbookFriends;
    bool showFriendsbookInfo;
    bool showFriendsbookPosts;
    Vector2 addFriendsbookFriendsScrollPosition;
    Vector2 friendsbookCurrentFriendsScrollPosition;

    //style variables for listItems
    GUIStyle listItemStyle = new GUIStyle();
    Color[] colors = new Color[] { Color.white, Color.grey };

    //style for margins
    GUIStyle marginStyle = new GUIStyle();


    //call this to instantiate editor window
    public static void Init(Character c, string folderPath)
    {
        //getwindow, attempt to dock next to existing CharacterListEditor
        CharacterEditor w = EditorWindow.GetWindow<CharacterEditor>(typeof(CharacterListEditor));
        w.character = c;
        w.folderPath = folderPath;

        //init editable strings
        w.SetStrings(c);
    }

    private void OnGUI()
    {
        if(character == null)
        {
            Debug.LogError("No character in CharacterEditor...", this);
        }
        else
        {
            //textfield for all editable fields
            GUILayout.Label(character.fullName, EditorStyles.largeLabel);

            //entire window should be scrollable
            mainScrollPosition = GUILayout.BeginScrollView(mainScrollPosition);


            GUILayout.Label("Personal Information", EditorStyles.boldLabel);
            fullName = EditorGUILayout.TextField("Full name: ", fullName);
            address = EditorGUILayout.TextField("Address: ", address);
            email = EditorGUILayout.TextField("Email: ", email);
            phoneNumber = EditorGUILayout.TextField("Phone number: ", phoneNumber);

            //buttons for saving / reverting changes
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save", GUILayout.ExpandWidth(false)))
            {
                SaveCharacterInformation();
            }
            if (GUILayout.Button("Revert", GUILayout.ExpandWidth(false)))
            {
                RevertCharacterInformation();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);

            //Friendsbook data
            showFriendsbook = EditorGUILayout.Foldout(showFriendsbook, "Friendsbook Profile", true);
            if (showFriendsbook)
            {
                marginStyle.margin.left = 10;
                GUILayout.BeginVertical(marginStyle); //wraps friendsbook view
                if (character.friendsbookProfile == null)
                {
                    GUILayout.Label("No Friendsbook profile attached. ");
                    if (GUILayout.Button("Create Profile", GUILayout.ExpandWidth(false)))
                    {
                        //create friendsbookprofile asset at folderpath
                        InitFriendsbookProfile();
                    }
                }
                else
                {
                    //editor for friendsbook profile.
                    //Contains foldout for settings, friends, info and posts

                    showFriendsbookSettings = EditorGUILayout.Foldout(showFriendsbookSettings, "Settings", true);
                    if (showFriendsbookSettings)
                    {
                        GUILayout.Label("Settings here. ");
                    }

                    showFriendsbookFriends = EditorGUILayout.Foldout(showFriendsbookFriends, "Friends", true);
                    if (showFriendsbookFriends)
                    {
                        GUILayout.Label("Add friend", EditorStyles.boldLabel);
                        //display all possible friends to add (using search??)
                        GUILayout.Label("Note: Only characters with attached Friendsbook profiles appear here.", EditorStyles.helpBox);

                        //Search on name
                        GUILayout.BeginHorizontal(EditorStyles.toolbar);
                        GUILayout.Label("Search: ");
                        charactersSearchString = EditorGUILayout.TextField(charactersSearchString);
                        GUILayout.EndHorizontal();

                        //filter characters on characters matching searchstring AND having friendsbookprofile
                        List<Character> filteredCharacters = character.characterList.list.FindAll(c => c.fullName.ToLower().Contains(charactersSearchString.ToLower()) && (c.friendsbookProfile != null));

                        addFriendsbookFriendsScrollPosition = GUILayout.BeginScrollView(addFriendsbookFriendsScrollPosition, false, true, GUILayout.Height(100));
                        for (int i = 0; i < filteredCharacters.Count; i++)
                        {
                            Character c = filteredCharacters[i];
                            if (c != character)
                            {
                                listItemStyle.normal.background = MakeTex(1, 1, colors[i % 2]); //used to alternate background colors
                                GUILayout.BeginHorizontal(listItemStyle);
                                GUILayout.Label(c.fullName);
                                if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
                                {
                                    //Add friendship in both characters
                                    AddFriendship(c);
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndScrollView();

                        GUILayout.Label("Current friends", EditorStyles.boldLabel);
                        if (character.friendsbookProfile.HasFriends())
                        {
                            //Search on name
                            GUILayout.BeginHorizontal(EditorStyles.toolbar);
                            GUILayout.Label("Search: ");
                            currentFriendsSearchString = EditorGUILayout.TextField(currentFriendsSearchString);
                            GUILayout.EndHorizontal();

                            //filter characters on searchstring
                            List<Character> filteredFriends = character.friendsbookProfile.friends.FindAll(c => c.fullName.ToLower().Contains(currentFriendsSearchString.ToLower()));


                            //display all current friends (+ possibility to remove friendship)
                            friendsbookCurrentFriendsScrollPosition = GUILayout.BeginScrollView(friendsbookCurrentFriendsScrollPosition, false, true, GUILayout.Height(100));

                            for (int i = 0; i < filteredFriends.Count; i++)
                            {
                                Character c = filteredFriends[i];
                                listItemStyle.normal.background = MakeTex(1, 1, colors[i % 2]); //used to alternate background colors
                                GUILayout.BeginHorizontal(listItemStyle);
                                GUILayout.Label(c.fullName);
                                if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                                {
                                    //remove friendship in both characters
                                    RemoveFriendship(c);
                                }
                                GUILayout.EndHorizontal();

                            }
                            GUILayout.EndScrollView();
                        }
                        else
                        {
                            GUILayout.Label("No current friends.");
                        }



                    }

                    showFriendsbookInfo = EditorGUILayout.Foldout(showFriendsbookInfo, "Info", true);
                    if (showFriendsbookInfo)
                    {
                        GUILayout.Label("Info here. ");
                    }

                    showFriendsbookPosts = EditorGUILayout.Foldout(showFriendsbookPosts, "Posts", true);
                    if (showFriendsbookPosts)
                    {
                        GUILayout.Label("Posts here. ");
                    }
                }
                GUILayout.EndVertical();
            }

            //Add foldouts for other apps as needed
            
            GUILayout.EndScrollView();
        }
    }

    private void SaveCharacterInformation()
    {
        character.fullName = fullName;
        character.address = address;
        character.phoneNumber = phoneNumber;
        character.email = email;
        GUI.FocusControl(null);
        EditorUtility.SetDirty(character);
    }

    private void RevertCharacterInformation()
    {
        SetStrings(character);
        GUI.FocusControl(null);
    }

    //sets all strings used in editable fields to corresponding strings in character
    private void SetStrings(Character c) 
    {
        fullName = c.fullName;
        address = c.address;
        email = c.email;
        phoneNumber = c.phoneNumber;
    }

    //Creates new friendsbookprofile asset, and assigns this to character
    void InitFriendsbookProfile()
    {
        var p = CreateFriendsbookProfile.Create(character, folderPath, character.fullName);
        character.friendsbookProfile = p;
        EditorUtility.SetDirty(character);
    }

    void AddFriendship(Character c)
    {
        try
        {
            character.friendsbookProfile.AddFriend(c);
            c.friendsbookProfile.AddFriend(character);
            GUI.FocusControl(null);
            //set profiles dirty to ensure changes are saved
            EditorUtility.SetDirty(character.friendsbookProfile);
            EditorUtility.SetDirty(c.friendsbookProfile);
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Missing friendsbookProfile reference in character " + c.fullName);
            Debug.LogException(e);
        }

        
    }

    //removes friendship in both this.character and c
    void RemoveFriendship(Character c)
    {
        character.friendsbookProfile.RemoveFriend(c);
        c.friendsbookProfile.RemoveFriend(character);
        //set profiles dirty to ensure changes are saved
        EditorUtility.SetDirty(character.friendsbookProfile);
        EditorUtility.SetDirty(c.friendsbookProfile);
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

}
