using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    public GameObject AppSettingsPanelPrefab;
    public GameObject BaseCanvasPrefab;
    public GameObject PhoneSettingsInfoPrefab;

    public List<string> appNames = new List<string>();
    private int currentIndex;
    private DropDownItemController ddic;
    private GameObject AppSettingsPanel;
    private GameObject PhoneSettingsInfo;

    // Use this for initialization
    void Start () {
        currentIndex = 0;
        AppSettingsPanel = Instantiate(AppSettingsPanelPrefab,BaseCanvasPrefab.transform); //Instantiate prefab
        PhoneSettingsInfo = Instantiate(PhoneSettingsInfoPrefab, BaseCanvasPrefab.transform); //instantiate Prefab
        //AppSettingsPanel.SetParent(BaseCanvasPrefab, false); //making sure the right canvas object is parrent (phoneprefab)
        //PhoneSettingsInfo.SetParent(BaseCanvasPrefab,false); //making sure the right canvas object is parrent (phoneprefab)
        ddic = AppSettingsPanel.GetComponent<DropDownItemController>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void moveToNextAppSetting_onClick()
    {
        GameObject tempAppSettingsPanel = GameObject.Find("AppSettingsPanel");
        currentIndex = currentIndex + 1;
        GameObject.Destroy(tempAppSettingsPanel);
        AppSettingsPanel = Instantiate(AppSettingsPanelPrefab, BaseCanvasPrefab.transform);
    }

    public string GetAppSettingName()
    {
        GameObject tempAppSettingsPanel = GameObject.Find("AppSettingsPanel");
        ddic = tempAppSettingsPanel.GetComponent<DropDownItemController>();
        //return ddic.settingAppNameText.text;
        return "TODO: Fix SettingsController.cs string return..";
    }

    public void SetAppSettingName(int index)
    {
        GameObject tempAppSettingsPanel = GameObject.Find("AppSettingsPanel");
        ddic = tempAppSettingsPanel.GetComponent<DropDownItemController>();
        //ddic.settingAppNameText.text = appNames[index];

    }




}
