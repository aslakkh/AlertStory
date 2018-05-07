using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Settings;
using UnityEngine.SceneManagement;

public class DropDownItemController : MonoBehaviour {

    public Dropdown settingsFacebookDropdown;// The dropdown object in the scene.
    public Dropdown settingsInstabartDropdown;// The dropdown object in the scene.
    public Dropdown settingsFinnDropdown;// The dropdown object in the scene.
    public Dropdown settingsTrainerDropdown;// The dropdown object in the scene.
    public Dropdown settingsSnapDropdown;// The dropdown object in the scene.


    public Text settingsFacebookInformationText; //UI-element object in the scene. Text related to the setting chosen.
    public Text settingsInstabartInformationText; //UI-element object in the scene. Text related to the setting chosen.
    public Text settingsFinnInformationText; //UI-element object in the scene. Text related to the setting chosen.
    public Text settingsTrainerInformationText; //UI-element object in the scene. Text related to the setting chosen.
    public Text settingsSnapInformationText; //UI-element object in the scene. Text related to the setting chosen.


    public Text settingFacebookAppNameText; //UI-element object in the scene. Text related to the the current name of the app.
    public Text settingInstabartAppNameText; //UI-element object in the scene. Text related to the the current name of the app.
    public Text settingFinnAppNameText; //UI-element object in the scene. Text related to the the current name of the app.
    public Text settingTrainerAppNameText; //UI-element object in the scene. Text related to the the current name of the app.
    public Text settingSnapAppNameText; //UI-element object in the scene. Text related to the the current name of the app.

    public bool validated = false;


    // Dict with the setting name and its description.
    private Dictionary<string, string> settingsInfo = new Dictionary<string, string>() {
        {"Private","Private mean noone else can see the actions you do in the app, but yourself" },
        {"Friends","Friends mean that you and the friends you have in the app can see you actions there."},
        {"Public", "Public is the setting which makes any other use of the same app, able to see your actions in that app." }
    };

    // Use this for initialization
    void Start() {
        List<string> tempSettings = new List<string>(settingsInfo.Keys);
        tempSettings.Add("Select option");
        tempSettings.Reverse();
        populatelist(settingsFacebookDropdown, tempSettings);
        populatelist(settingsInstabartDropdown, tempSettings);
        populatelist(settingsFinnDropdown, tempSettings);
        populatelist(settingsTrainerDropdown, tempSettings);
        populatelist(settingsSnapDropdown, tempSettings);
        settingsFacebookInformationText.text = "Please choose a option for the app.";
        settingsFacebookInformationText.color = Color.red;
        settingsInstabartInformationText.text = "Please choose a option for the app.";
        settingsInstabartInformationText.color = Color.red;
        settingsFinnInformationText.text = "Please choose a option for the app.";
        settingsFinnInformationText.color = Color.red;
        settingsTrainerInformationText.text = "Please choose a option for the app.";
        settingsTrainerInformationText.color = Color.red;
        settingsSnapInformationText.text = "Please choose a option for the app.";
        settingsSnapInformationText.color = Color.red;
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
    public void settingsDropDownInstabartOnValueCHanged(int index)
    {
        settingsDropdown_OnIndexChanged(settingsInstabartInformationText, index);
    }
    public void settingsDropDownFinnOnValueCHanged(int index)
    {
        settingsDropdown_OnIndexChanged(settingsFinnInformationText, index);
    }
    public void settingsDropDownTrainerOnValueCHanged(int index)
    {
        settingsDropdown_OnIndexChanged(settingsTrainerInformationText, index);
    }
    public void settingsDropDownSnapOnValueCHanged(int index)
    {
        settingsDropdown_OnIndexChanged(settingsSnapInformationText, index);
    }

    public void settingsDropdown_OnIndexChanged(Text text, int index)
    {

        if (index == 0)
        {
            text.text = "Please choose a option for the app.";
            text.color = Color.red;
        }
        else if (index == 1)
        {
            text.text = settingsInfo["Public"];
            text.color = Color.black;
        }
        else if (index == 2)
        {
            text.text = settingsInfo["Friends"];
            text.color = Color.black;
        }
        else if (index == 3)
        {
            text.text = settingsInfo["Private"];
            text.color = Color.black;
        }
        else
        {
            text.text = "Please choose a option for the  app.";
            text.color = Color.red;
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
            // change scene after saving the requirements.
            SceneManager.LoadScene("TEMP_EventsScene");
        }
        // if no dropdowns are present, change scene anyway. 
        SceneManager.LoadScene("TEMP_EventsScene");
    }


    // Saves the requirements in the gamemanager script in the gamemanager object.
    public void saveRequirements()
    {
        GameObject gm = GameObject.Find("GameManager");
        GameManager gamemanager = gm.GetComponent<GameManager>();
        GameObject tempObj = GameObject.Find("AppSettingsContent");

        // loop trough all dropdowns and store their values.
        foreach (Dropdown d in GameObject.FindObjectsOfType<Dropdown>()) {
            Transform text = d.transform.parent.GetChild(1);
            if (text.name.Contains("NameText")) {
                Requirement temp = new Requirement(text.GetComponent<Text>().text);
                if (d.value == 1)
                {
                    gamemanager.requirements.Add(temp.requirementName, Setting.Public);
                }
                if (d.value == 2)
                {
                    gamemanager.requirements.Add(temp.requirementName, Setting.Friends);
                }
                if (d.value == 3)
                {
                    gamemanager.requirements.Add(temp.requirementName, Setting.Private);
                }

            }
        }
    }
}
