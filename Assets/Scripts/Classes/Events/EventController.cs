using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EventController : MonoBehaviour {

    //public static GameObject eventPrefab;

    protected GameManager gameManager;    // gameManager reference is used to propagate score changes to gamemanager. Should look at using unity's messaging system instead to prevent too many dependencies

    private StoryEvent _storyEvent;
    public StoryEvent storyEvent
    {
        get { return _storyEvent; }
        set { _storyEvent = value; }
    }

    public void DestroyFromScene()
    {
        Destroy(gameObject);
    }
}
