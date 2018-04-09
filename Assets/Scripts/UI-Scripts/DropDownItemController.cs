using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    // Dict with the setting name and its description.
    private Dictionary<string, string> settingsInfo = new Dictionary<string, string>() {
        {"Private","Private mean noone else can see the actions you do in the app, but yourself" },
        {"Friends","Friends mean that you and the friends you have in the app can see you actions there."},
        {"Public", "Public is the setting which makes any other use of the same app, able to see your actions in that app." }
    };

	// Use this for initialization
	void Start () {
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
    public void settingsDropdown_OnIndexChanged(int index)
    {

        GameObject temp = GameObject.Find("AppSettingFacebookInformationText");
        Text tempText = temp.GetComponentInChildren<Text>();
        tempText.text = "Hello world!";
        Debug.Log("In setting Dropdown On changed menu");
        /*
        if (index == 0)
        {
            this.text = "Please choose a option for the app.";
            settingsInformationText.color = Color.red;
        }
        else if (index == 1)
        {
            settingsInformationText.text = settingsInfo["Private"];
            settingsInformationText.color = Color.black;
        }
        else if (index == 2)
        {
            settingsInformationText.text = settingsInfo["Friends"];
            settingsInformationText.color = Color.black;
        }
        else if (index == 3)
        {
            settingsInformationText.text = settingsInfo["Public"];
            settingsInformationText.color = Color.black;
        }
        else
        {
            settingsInformationText.text = "Please choose a option for the  app.";
            settingsInformationText.color = Color.red;
        }
        */
    }

    // Validates that the dropbox have a valid option selected.Useful to check before changing scenes. 
    private bool validateDropDownChoice(Dropdown x)
    {
        int val = x.value;
        if (val < 1 && val > 3)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
