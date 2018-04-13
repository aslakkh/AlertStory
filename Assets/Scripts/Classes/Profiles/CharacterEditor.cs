using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow {

    public Character character;
    //public CharacterList characterList;

    //all character attributes 
    public string firstName;
    public string lastName;
    public string address;
    public string email;
    public string phoneNumber;

    //call this to instantiate editor window
    public static void Init(Character c)
    {
        //getwindow, attempt to dock next to existing CharacterListEditor
        CharacterEditor w = EditorWindow.GetWindow<CharacterEditor>(typeof(CharacterListEditor));
        w.character = c;

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
            GUILayout.Label(string.Format("{0} {1}", character.firstName, character.lastName), EditorStyles.largeLabel);
            GUILayout.Label("Personal Information", EditorStyles.boldLabel);
            firstName = EditorGUILayout.TextField("First name: ", firstName);
            lastName = EditorGUILayout.TextField("Last name: ", lastName);
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
            GUILayout.Label("Friendsbook Profile", EditorStyles.boldLabel);

            if (character.friendsbookProfile == null)
            {
                GUILayout.Label("No Friendsbook profile attached. ");
                if (GUILayout.Button("Create Profile", GUILayout.ExpandWidth(false)))
                {
                    
                }
            }
            else
            {
                //editor for friendsbook profile.
                //should allow editing of settings, friends, editing of posts, and editing of facebook info
            }
        }
    }

    private void SaveCharacterInformation()
    {
        character.firstName = firstName;
        character.lastName = lastName;
        character.address = address;
        character.phoneNumber = phoneNumber;
        character.email = email;
        GUI.FocusControl(null);
    }

    private void RevertCharacterInformation()
    {
        SetStrings(character);
        GUI.FocusControl(null);
    }

    //sets all strings used in editable fields to corresponding strings in character
    private void SetStrings(Character c) 
    {
        firstName = c.firstName;
        lastName = c.lastName;
        address = c.address;
        email = c.email;
        phoneNumber = c.phoneNumber;
    }
	
}
