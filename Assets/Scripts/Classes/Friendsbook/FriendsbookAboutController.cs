using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsbookAboutController : MonoBehaviour {

    public Text nameText;
    public Text addressText;
    public Text emailText;
    public Text phoneNumberText;

	public void SetInformation(Character c)
    {
        nameText.text = c.fullName;
        addressText.text = c.address;
        emailText.text = c.email;
        phoneNumberText.text = c.phoneNumber;
    }
}
