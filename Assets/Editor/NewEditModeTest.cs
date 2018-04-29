using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameManagerEditModeTest {

    // Accessing the GameManager Script, and use the first one it finds, in all tests.
    public GameManager gm = GameObject.FindObjectOfType<GameManager>();

    [Test]
    [Description("Test the initial score value to be 0 and validates that it is a int")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth", "olejsc@stud.ntnu.no")]
    public void CheckIfScoreIsZero()
    {
        // Use the Assert class to test conditions.
        Assert.That<int>(gm.score, Is.Zero);
    }

    [Test]
    [Description("Test the initial turnCount value to be 0")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth")]
    public void CheckTurnCountIsZero()
    {
        Assert.That(gm.turnCount.Equals(0), "Score is not null in GameManager");
    }

    [Test]
    [Description("Test the initial dayCount value to be 0")]
    [Category("Unit test")]
    [Author("Ole Jakob Schjøth")]
    public void CheckDayCountIsZero()
    {
        Assert.That(gm.dayCount.Equals(0), "Score is not null in GameManager");
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
