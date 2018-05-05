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

    public GameObject informationPackage;
    public InformationPackageController informationPackageController;
    public Button nameButton;


    void Awake () {
        informationPackage = GameObject.Find("InformationPackage");
        Debug.Log(informationPackage);
        informationPackageController.GetComponent<InformationPackageController>();
    }
    void Start () {
        nameButton.onClick.AddListener(delegate () { informationPackageController.AddPersonToInformationPackage(nameText.text); });
    }

	public void SetInformation(Character c)
    {
        nameText.text = string.IsNullOrEmpty(c.fullName) ? "NA" : c.fullName;
        addressText.text = string.IsNullOrEmpty(c.address) ? "NA": c.address;
        emailText.text = string.IsNullOrEmpty(c.email) ? "NA" : c.email;
        phoneNumberText.text = string.IsNullOrEmpty(c.phoneNumber) ? "NA" : c.phoneNumber;
    }
}
