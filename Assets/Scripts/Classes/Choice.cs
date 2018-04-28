using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Choice{
    public string choiceDescription;
    public int affectScorePrivate;
    public int affectScoreFriends;
    public int affectScorePublic;
    public StoryEvent triggersStoryEventPrivate;
    public StoryEvent triggersStoryEventFriends;
    public StoryEvent triggersStoryEventPublic;
    
    
    public Choice() {
        choiceDescription = "Blank";
        affectScorePrivate = affectScoreFriends = affectScorePublic = 0;
        triggersStoryEventPrivate = triggersStoryEventFriends = triggersStoryEventPublic = null;
    }


}
