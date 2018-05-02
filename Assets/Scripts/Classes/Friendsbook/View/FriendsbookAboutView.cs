using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//friendsbook "about"-view (subview of person profile-view)
public class FriendsbookAboutView : MonoBehaviour {

    public Text nameText;
    public Text addressText;
    public Text emailText;
    public Text phoneNumberText;

	public void SetInformation(Character c)
    {
        nameText.text = string.IsNullOrEmpty(c.fullName) ? "NA" : c.fullName;
        addressText.text = string.IsNullOrEmpty(c.address) ? "NA": c.address;
        emailText.text = string.IsNullOrEmpty(c.email) ? "NA" : c.email;
        phoneNumberText.text = string.IsNullOrEmpty(c.phoneNumber) ? "NA" : c.phoneNumber;
    }
}
