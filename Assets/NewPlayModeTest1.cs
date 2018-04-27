using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TrainPlayModeTest
{
    private string initialScenePath;
    [SetUp]
    public void Setup()
    {
        Debug.Log("Load Scene");
        initialScenePath = SceneManager.GetActiveScene().path;
        SceneManager.LoadScene("Scenes/AppSettingsScene");
    }

    [TearDown]
    public void TearDown()
    {
        SceneManager.LoadScene(initialScenePath);
    }

    [UnityTest]
    public IEnumerator TrainPlayModeTestWithEnumeratorPasses2()
    {
        Debug.Log("**The test started**");
        GameObject gm = new GameObject();
        gm.AddComponent<GameManager>();
        var OriginalScore = gm.GetComponent<GameManager>().score;
        gm.GetComponent<GameManager>().score = 100;
        yield return new WaitForFixedUpdate();
        var NewScore = gm.GetComponent<GameManager>().score;
        Assert.AreNotEqual(NewScore, OriginalScore);
    }
}
