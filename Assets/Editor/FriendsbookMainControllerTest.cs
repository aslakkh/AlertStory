using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;

public class FriendsbookMainControllerTest {

    [Test]
    [Description("InstantiateCurrentViewPrefab returns new instance")]
    [Category("Unit test")]
    public void InstantiatedViewIsGameObject()
    {
        var gameObject = new GameObject();
        FriendsbookMainController controller = gameObject.AddComponent<FriendsbookMainController>();
        var instance = controller.InstantiateCurrentViewFromPrefab(new GameObject());
        Assert.IsNotNull(instance, "Instantiated current view is null.");
        Assert.That(instance.GetType().Equals(new GameObject().GetType()), "Instantiated current view is not of type gameobject");
    }

    [Test]
    [Description("InstantiateCurrentViewPrefab instantiates currentview as child of its controller")]
    [Category("Unit test")]
    public void InstantiatedViewIsChildOfController()
    {
        var gameObject = new GameObject();
        FriendsbookMainController controller = gameObject.AddComponent<FriendsbookMainController>();
        var instance = controller.InstantiateCurrentViewFromPrefab(new GameObject());
        Assert.That(instance.transform.IsChildOf(controller.transform), "Instantiated current view is not parent of controller");
    }

    [Test]
    [Description("InstantiateCurrentViewPrefab should instantiate currentview and set it as first sibling")]
    [Category("Unit test")]
    public void InstantiatedViewIsFirstSibling()
    {
        //assert that instance is first sibling of empty parent
        var gameObject = new GameObject("Controller");
        FriendsbookMainController controller = gameObject.AddComponent<FriendsbookMainController>();
        var instance = controller.InstantiateCurrentViewFromPrefab(new GameObject("Prefab"));
        Assert.That(instance.transform.parent.GetChild(0).Equals(instance.transform), "Instantiated current view is not first sibling of empty parent");

        //assert that instance is last sibling of parent with preexisting children
        gameObject = new GameObject("Controller");
        controller = gameObject.AddComponent<FriendsbookMainController>();
        var mock = new GameObject();
        mock.transform.SetParent(controller.transform);
        var secondInstance = controller.InstantiateCurrentViewFromPrefab(new GameObject("Prefab"));
        Assert.That(secondInstance.transform.parent.GetChild(0).Equals(secondInstance.transform), "Instantiated current view is not first sibling of populated parent");

    }

}
