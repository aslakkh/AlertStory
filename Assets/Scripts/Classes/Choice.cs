using UnityEngine;
using System;

[Serializable]
public class Choice
{
    public string choiceDescription;
    public int affectScorePrivate;
    public int affectScoreFriends;
    public int affectScorePublic;
    public StoryEvent triggersStoryEventPrivate;
    public StoryEvent triggersStoryEventFriends;
    public StoryEvent triggersStoryEventPublic;

}