using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Settings;

public class FriendsbookPersonControllerTest : MonoBehaviour {

    public Character MockCharacterWithPrivateSettings()
    {
        var c = ScriptableObject.CreateInstance<Character>();
        c.friendsbookProfile = ScriptableObject.CreateInstance<FriendsbookProfile>();
        c.friendsbookProfile.informationSetting = Setting.Private;
        c.friendsbookProfile.friendsSetting = Setting.Private;
        c.friendsbookProfile.postsSetting = Setting.Private;
        return c;
    }

    public Character MockCharacterWithFriendsSettings()
    {
        var c = ScriptableObject.CreateInstance<Character>();
        c.friendsbookProfile = ScriptableObject.CreateInstance<FriendsbookProfile>();
        c.friendsbookProfile.informationSetting = Setting.Friends;
        c.friendsbookProfile.friendsSetting = Setting.Friends;
        c.friendsbookProfile.postsSetting = Setting.Friends;
        return c;
    }

    public Character MockCharacter()
    {
        var c = ScriptableObject.CreateInstance<Character>();
        return c;
    }

    [Test]
    [Description("Tests if friendsbook person controller sets setting to the settings of character")]
    [Category("Unit Test")]
    public void ControllerCorrectlyInitiatesPageSettings()
    {
        //arrange
        var element = new GameObject("temp").AddComponent<FriendsbookPersonController>();
        var c = MockCharacterWithPrivateSettings();
        element.SetCharacter(c);

        //act
        element.SetSettings(true);

        //assert that settings are initiated correctly
        Assert.AreEqual(element.GetFriendsSetting(), c.friendsbookProfile.friendsSetting, "Component friends setting did not match character friends setting.");
        Assert.AreEqual(element.GetInfoSetting(), c.friendsbookProfile.informationSetting, "Component info setting did not match character info setting.");
        Assert.AreEqual(element.GetPostsSetting(), c.friendsbookProfile.postsSetting, "Component posts setting did not match character posts setting.");

    }


    [Test]
    [Description("Tests that settings are set to public if character has settings set to 'friends' and is friends with playercharacter")]
    [Category("Unit Test")]
    public void SettingsSetToFriendsBehaveAsPublicIfCharactersAreFriends()
    {
        //arrange
        var element = new GameObject("temp").AddComponent<FriendsbookPersonController>();
        var c = MockCharacterWithFriendsSettings();
        element.SetCharacter(c);

        //act
        element.SetSettings(true);

        //assert
        Assert.AreEqual(element.GetFriendsSetting(), Setting.Public, "Component friends setting should be public when character are friends and character settings are set to 'Friends'");
        Assert.AreEqual(element.GetInfoSetting(), Setting.Public, "Component info setting should be public when character are friends and character settings are set to 'Friends.");
        Assert.AreEqual(element.GetPostsSetting(), Setting.Public, "Component posts setting should be public when character are friends and character settings are set to 'Friends.");

        element.SetSettings(false);
        Assert.AreEqual(element.GetFriendsSetting(), Setting.Friends, "Component friends setting should be friends when character are not friends and character settings are set to 'Friends'");
        Assert.AreEqual(element.GetInfoSetting(), Setting.Friends, "Component info setting should be friends when character are not friends and character settings are set to 'Friends.");
        Assert.AreEqual(element.GetPostsSetting(), Setting.Friends, "Component posts setting should be friends when character are not friends and character settings are set to 'Friends.");
    }

    [Test]
    [Description("Tests that trying to set character of controller to a character without friendsbookprofile throws an error message")]
    [Category("Unit Test")]
    public void SettingCharacterWithoutFriendsbookProfileThrowsError()
    {
        //arrange
        var element = new GameObject("temp").AddComponent<FriendsbookPersonController>();
        var c = MockCharacter();

        //act
        element.SetCharacter(c);

        //assert
        LogAssert.Expect(LogType.Error, "Tried setting character of friendsbookPersonController to character without friendsbook profile");
    }

    [Test]
    [Description("Test that character is set if character has friendsbook profile")]
    [Category("Unit test")]
    public void SuccessfullySetsCharacterIfCharacterHasProfile()
    {
        //arrange
        var element = new GameObject("temp").AddComponent<FriendsbookPersonController>();
        var c = MockCharacterWithPrivateSettings();

        //act
        element.SetCharacter(c);

        Assert.AreEqual(element.GetCharacter(), c, "Character not set successfully");
    }


    [Test]
    [Description("Test that the subview is correctly displayed based on the character's settings")]
    [Category("Unit test")]
    public void DisplaysCorrectAboutViewBasedOnCharacterSettings()
    {
        //arrange
        var element = new GameObject("temp").AddComponent<FriendsbookPersonController>();
        var viewGameObject = new GameObject("view");
        element.viewComponent = viewGameObject.AddComponent<FriendsbookPersonView>();
        element.viewComponent.scrollRect = viewGameObject.AddComponent<ScrollRect>();
        element.hiddenViewPrefab = new GameObject("hiddenViewPrefab");
        element.aboutViewPrefab = new GameObject("aboutViewPrefab");
        element.friendsViewPrefab = new GameObject("friendsViewPrefab");
        Character c = MockCharacterWithPrivateSettings();
        element.SetCharacter(c);

        //act
        element.SetSettings(true);
        element.DisplayAboutView();

        //assert that hiddenviewprefab is displayed if character has informationSetting = Setting.Private
        Assert.IsNotNull(element.GetCurrentSubView(), "Current sub view was null, should display about view");
        Assert.AreEqual(element.GetCurrentSubView().name, element.hiddenViewPrefab.name + "(Clone)", "current subview was not clone of hiddenviewprefab");

        element.SetCharacter(MockCharacterWithFriendsSettings());
        element.SetSettings(false);
        element.DisplayAboutView();
        //assert that hiddenviewprefab is displayed when characters are not friends
        Assert.AreEqual(element.GetCurrentSubView().name, element.hiddenViewPrefab.name + "(Clone)", "current subview was not clone of hiddenviewprefab");

        element.SetSettings(true);
        element.DisplayAboutView();

    }

    //[Test]
    //[Description("")]
    //[Category("")]
}
