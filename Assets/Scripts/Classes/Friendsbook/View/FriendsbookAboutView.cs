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
    private int day = GameManager.Instance.dayCount;
    private List<string> currentDay;

    private void Start()
    {
        currentDay = GameManager.Instance.informationPackage;
    }

    public void SetInformation(Character c)
    {
        nameText.text = string.IsNullOrEmpty(c.fullName) ? "NA" : c.fullName;
        addressText.text = string.IsNullOrEmpty(c.address) ? "NA": c.address;
        emailText.text = string.IsNullOrEmpty(c.email) ? "NA" : c.email;
        phoneNumberText.text = string.IsNullOrEmpty(c.phoneNumber) ? "NA" : c.phoneNumber;
    }

    public void OnNameClicked()
    {
        Debug.Log(currentDay);
        Debug.Log(nameText.text);
        if (!nameText.text.Equals("NA"))
        {
            currentDay.Add(nameText.text);
        }
    }

    public void OnAddressClicked()
    {
        Debug.Log(addressText.text);
        if (!addressText.text.Equals("NA"))
        {
            currentDay.Add(addressText.text);
        }
        printList();
    }

    public void OnEmailClicked()
    {
        Debug.Log(emailText.text);
        if (!emailText.text.Equals("NA"))
        {
            currentDay.Add(emailText.text);
        }
    }

    public void OnPhoneNumberClicked()
    {
        if (!phoneNumberText.text.Equals("NA"))
        {
            currentDay.Add(phoneNumberText.text);
        }
    }

    public void printList()
    {
        foreach (string element in currentDay)
        {
            Debug.Log(element);
        }
    }
}
