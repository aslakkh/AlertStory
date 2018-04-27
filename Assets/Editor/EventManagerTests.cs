using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class EventManagerTests {

    

    [Test]
    [Description("FindFirstRelevantEvent returns null for empty list.")]
    [Category("Unit test")]
    [Author("Aslak Karlsen Hauglid", "aslakkh@stud.ntnu.no")]
    public void FindFirstRelevantEvent_WillReturnNull()
    {
        // Use the Assert class to test conditions.
        GameObject gameObject = new GameObject("em");
        gameObject.AddComponent<EventManager>();
        EventManager eventManager = gameObject.GetComponent<EventManager>();
        List<StoryEvent> storyEvents = new List<StoryEvent>();
        Assert.That<StoryEvent>(eventManager.FindFirstRelevantEvent(storyEvents), Is.Null);
    }
}
