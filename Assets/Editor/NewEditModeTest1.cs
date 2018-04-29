using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

public class ChoiceScriptEditModeTest {

    // Accessing various scripts and creating instances of them.
    public Choice ch;
    private StoryEvent se;
    private List <Choice> choiceList = new List<Choice>();
    private RequirementsList rL;
    private Dependencies dp;
    public GameObject obj;

    [SetUp]
    [Description("Used to setup the variables for testing")]
    public void SetUp()
    {
        //se = new StoryEvent("Another String", choiceList, rL, dp);
        se = new StoryEvent();
        ch = new Choice();
    }


    // Failing tests.
    [Test]
    [Description("Test the Choice string to be 'someString'")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckChoiceStringDescription()
    {
        Assert.That(ch.choiceDescription, Does.Contain("someString"));
    }


    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewEditModeTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
