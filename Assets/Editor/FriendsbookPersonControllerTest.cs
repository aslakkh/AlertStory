using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;

public class FriendsbookPersonControllerTest : MonoBehaviour {

    public Character CharacterMock()
    {
        var c = ScriptableObject.CreateInstance<Character>();
        c.name = "Mock";
        c.address = "Mock";
        c.email = "Mock";
        c.phoneNumber = "123";
        return c;
    }

	//[Test]
 //   [Description("")]
 //   [Category("")]


 //   [Test]
 //   [Description("")]
 //   [Category("")]
}
