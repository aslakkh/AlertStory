using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class AppSettingsSceneUnitTests {

    public Dropdown[] Dropdowns = GameObject.FindObjectsOfType<Dropdown>();
    [SetUp]
    [Description("Used to setup the variables for testing")]
    public void SetUp()
    {
    }


    [Test]
    [Description("Check that the facebook dropdown exists in the scene.")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckFacebokDropDownValue()
    {
        bool result = false;
        foreach(Dropdown D in Dropdowns)
        {
            if (D.name == "AppSettingsFacebookDropdown")
            {
                result = true;
                break;
            }
            else
            {
                continue;
            }
        }
        Assert.That(result.Equals(true), "There is no object called 'AppSettingsFacebokDropdown'");
    }

    [Test]
    [Description("Check that the Instabart dropdown object have a initial value of 0.")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckInstabartDropDownValue()
    {
        int result = -1;
        foreach (Dropdown D in Dropdowns)
        {
            if (D.name == "AppSettingsInstabartDropdown")
            {
                result = D.value;
                break;
            }
            else
            {
                continue;
            }
        }
        Assert.That(result.Equals(0), "There is no object called 'AppSettingsFacebokDropdown'");
    }

    [Test]
    [Description("Check that the Finn Text object have a value of 'Finn'.")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckFinnDropDownAppName()
    {
        string appName = "Finn.no";
        bool result = false;
        Text[] temp = GameObject.FindObjectsOfType<Text>();
        foreach (Text t in temp)
        {
            if (t.text == appName)
            {
                result = true;
                break;
            }
        }
        Assert.That(result.Equals(true), "There is no Text object with 'Finn' as its string value.");
    }

    [Test]
    [Description("Check that all dropdowns in the AppSettingsScene have only 4 options.")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckDropDownOptions()
    {
        int max = 4;
        int highest = 4;
        int lowest = 4;
        int min = 4;
        foreach (Dropdown D in Dropdowns)
        {
            int temp = D.options.Count;
            if (temp > max)
            {
                max = temp;
            }
            else if (temp < min)
            {
                temp = min;
            }
            else
            {
                continue;
            }
        }
        Assert.That(highest.Equals(4),"The highest number of options wasnt 4, but was actually higher:" + highest);
        Assert.That(lowest.Equals(4), "The lowest number of options wasnt 4, but was actually lower:" + lowest);
    }

    [Test]
    [Description("Check that the validation method works.")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckValidationMethodOnNextButton()
    {
        GameObject tempObj = GameObject.Find("AppSettingsContent");
        DropDownItemController ddic = tempObj.GetComponent<DropDownItemController>();
        bool preResult = ddic.validated;
        // At the instantiation of the scene, all dropdowns will have a value of 0 so the validation will not pass.
        Assert.That(preResult.Equals(false), "The initial validation value is True, it should be false.");
        ddic.validateDropDownChoices();
        // The validation value should still be 0 after the method has run: 
        Assert.That(ddic.validated.Equals(false), "The validation method didnt wrongly change this value to true.");
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator AppSettingsSceneUnitTestsWithEnumeratorPasses() {
        yield return null;
        // Use the Assert class to test conditions.
        GameManager gm = GameObject.Find("Gmanager").GetComponent<GameManager>();
        Assert.That(gm.score.Equals(0), "gm score is not zero");
		// yield to skip a frame
		yield return null;
	}
}
