using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownItemController : MonoBehaviour {

    public Dropdown settingsDropdown; // The dropdown object in the scene.
    public Text settingsInformationText; //UI-element object in the scene. Text related to the setting chosen.

    // Dict with the setting name and its description.
    private Dictionary<string, string> settingsInfo = new Dictionary<string, string>() {
        {"Private","Private mean noone else can see the actions you do in the app, but yourself" },
        {"Friends","Friends mean that you and the friends you have in the app can see you actions there."},
        {"Public", "Public is the setting which makes any other use of the same app, able to see your actions in that app." }
    };

	// Use this for initialization
	void Start () {
        populatelist();
    }
	

    //Populates the list of options in the dropdown.
    void populatelist()
    {
        List<string> tempSettings = new List<string>(settingsInfo.Keys);
        tempSettings.Add("Select option");
        tempSettings.Reverse(); // reverses the list, so the select option is on top.

        settingsDropdown.captionText.text = "Select option";
        settingsDropdown.AddOptions(tempSettings);

        settingsInformationText.text = "Please choose a option for the" + " APPNAME HERE" + " app.";
        settingsInformationText.color = Color.red;
    }

    // Updates the text corresponding to the option. Dictionairy approach with less if-conditions wasnt supported
    // in unity's event system.
    public void settingsDropdown_OnIndexChanged(int index)
    {
        if (index == 0)
        {
            settingsInformationText.text = "Please choose a option for the" + " APPNAME" + " app.";
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
            settingsInformationText.text = "Please choose a option for the" + " APPNAME HERE" + " app.";
            settingsInformationText.color = Color.red;
        }
    }

    // Validates that the dropbox have a valid option selected.Useful to check before changing scenes. 
    private bool validateDropDownChoice()
    {
        int val = settingsDropdown.value;
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
