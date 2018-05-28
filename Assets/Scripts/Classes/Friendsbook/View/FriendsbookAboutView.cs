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
    private List<string> currentDay;

    public GameObject informationPackage;
    public Button nameButton;
    private SoundManager soundManager;


    void Awake () {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();;
    }

    void Start () {
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
        soundManager.sfxSource.Play();
        if (!nameText.text.Equals("NA") && !currentDay.Contains(nameText.text))
        {
            currentDay.Add("Information:");
            currentDay.Add(nameText.text);
        }
    }

    public void OnAddressClicked()
    {
        soundManager.sfxSource.Play();
        if (!addressText.text.Equals("NA") && !currentDay.Contains(addressText.text))
        {
            currentDay.Add("Information:");
            currentDay.Add(addressText.text);
        }
    }

    public void OnEmailClicked()
    {
        soundManager.sfxSource.Play();
        if (!emailText.text.Equals("NA") && !currentDay.Contains(emailText.text))
        {
            currentDay.Add("Information:");
            currentDay.Add(emailText.text);
        }
    }

    public void OnPhoneNumberClicked()
    {
        soundManager.sfxSource.Play();
        if (!phoneNumberText.text.Equals("NA") && !currentDay.Contains(phoneNumberText.text))
        {
            currentDay.Add("Information:");
            currentDay.Add(phoneNumberText.text);
        }
    }
}
