﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Settings;
using UnityEngine.SceneManagement;

public class DropDownItemController : MonoBehaviour {

    private GameManager gm;

    public Dropdown settingsFacebookDropdown;// The dropdown object in the scene.

    public Text settingsFacebookInformationText; //UI-element object in the scene. Text related to the setting chosen.

    public Text settingFacebookAppNameText; //UI-element object in the scene. Text related to the the current name of the app.

    public bool validated = false;

    private string infoText = "Friendsbook is a social media platform. The settings you set refer to what and with who you share of your personal information. \n \nPersonal information includes name, adress, posts and more. Basically all you have on your own Friendsbook page. \n\nFriendsbook is where you want to announce your party. However, keep in mind what settings you choose to use as these will affect the choices you have to make in order to achieve the perfect party.";


    // Dict with the setting name and its description.
    private Dictionary<string, string> settingsInfo = new Dictionary<string, string>() {
        {"Private","When choosing private settings, you will not share your personal information with anyone." },
        {"Friends","When choosing friends settings, you will share your information with friends."},
        {"Public", "When choosing public settings, you will share your information with everyone." }
    };

    // Use this for initialization
    void Start() {
        List<string> tempSettings = new List<string>(settingsInfo.Keys);
        tempSettings.Add("Select an option");
        tempSettings.Reverse();
        populatelist(settingsFacebookDropdown, tempSettings);
        settingsFacebookInformationText.text = infoText;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    //Populates the list of options in the dropdown.
    void populatelist(Dropdown tempDropdown, List<string> settings)
    {
        tempDropdown.captionText.text = "Select option";
        tempDropdown.AddOptions(settings);
    }

    // Updates the text corresponding to the option. Dictionairy approach with less if-conditions wasnt supported
    // in unity's event system.

    public void settingsDropDownFacebookOnValueCHanged(int index)
    {
        settingsDropdown_OnIndexChanged(settingsFacebookInformationText, index);
    }

    public void settingsDropdown_OnIndexChanged(Text text, int index)
    {
        // Changes the information underneath the dropdown menu according to what is chosen
        if (index == 1)
        {
            text.text = settingsInfo["Public"];
        }
        else if (index == 2)
        {
            text.text = settingsInfo["Friends"];
        }
        else if (index == 3)
        {
            text.text = settingsInfo["Private"];
        }
        else
        {
            text.text = infoText;
        }

    }

    // Validates that the dropboxes have a valid option selected.Useful to check before changing scenes. 
    public void validateDropDownChoices()
    {
        // checks that each dropdown have a valid value before changing scene.
        foreach (Dropdown d in GameObject.FindObjectsOfType<Dropdown>())
        {
            if (d.value < 1 || d.value > 3)
            {
                validated = false;
                break;
            }
            else
            {
                validated = true;
                continue;
            }
        }
        if (validated == true)
        {
            saveRequirements();
        }
    }


    public void saveRequirements()
    {
        foreach (Dropdown d in GameObject.FindObjectsOfType<Dropdown>()) {
            Transform text = d.transform.parent.GetChild(1);
            if (text.name.Contains("NameText")) {
                Requirement temp = new Requirement(text.GetComponent<Text>().text);
                if (d.value == 1)
                {
                    gm.requirements.Add(temp.requirementName, Setting.Public);
                }
                if (d.value == 2)
                {
                    gm.requirements.Add(temp.requirementName, Setting.Friends);
                }
                if (d.value == 3)
                {
                    gm.requirements.Add(temp.requirementName, Setting.Private);
                }

            }
        }
        // Load first day
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadFirstDayScene();
    }
    
}
