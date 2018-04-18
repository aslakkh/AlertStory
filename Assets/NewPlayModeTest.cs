using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class NewPlayModeTest {

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    /*[UnityTest]
    public IEnumerator GameObject_WithRigidBody_WillBeAffectedByPhysics()
    {
        GameManager gm = GameObject.Find("Gmanager").GetComponent<GameManager>();
        int earlyScore = gm.score;
        yield return new WaitForSeconds(3);
        gm.score = 100;
        Assert.AreNotEqual(gm.score, earlyScore);
    }
    */


    [UnityTest]
    public IEnumerator GameObject_WithRigidBody2_WillBeAffectedByPhysics()
    {
        var go = new GameObject();
        go.AddComponent<GameManager>();
        var originalScore = go.GetComponent<GameManager>().score;

        yield return new WaitForFixedUpdate(); // Skips a frame. Important!

        go.GetComponent<GameManager>().score = 100;
        var newScore = go.GetComponent<GameManager>().score;
        Assert.AreNotEqual(originalScore, newScore,"The score is the same!, they sould be different");
        // To check if it works, uncomment the line below:
        //Assert.AreEqual(originalScore, newScore);
    }
}
