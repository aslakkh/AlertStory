using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class NewEditModeTest {

    public GameObject testobject;

    [Test]
	public void CheckForWhoaImage() {
        // Use the Assert class to test conditions.
        var go = GameObject.Find("whoa");
        Assert.IsNotNull(go, "No whoa object found in scene.");
    }

    [Test]
    public void CheckForPlayerManagerComponent()
    {
        // Use the Assert class to test conditions.
        var go = GameObject.Find("PlayerManager");
        Assert.IsTrue(go.GetComponent("PlayerController"),"Player Controller Component is not true");
    }

    [Test]
    public void CheckForPlayerManagerComponentNotNull()
    {
        // Use the Assert class to test conditions.
        var go = GameObject.Find("PlayerManager");
        var comp = go.GetComponent("PlayerController");
        Assert.IsNotNull(comp, "Component PlayerController is null");
    }

    [Test]
    public void CheckForPlayerManagerComponentName()
    {
        // Use the Assert class to test conditions.
        var go = GameObject.Find("PlayerManager");
        var comp = go.GetComponent("Player Controller");
        Debug.Log(comp.ToString());
        string name = "";
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
