using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    public Transform AppSettingsPanelPrefab;
    public Transform BaseCanvasPrefab;
    public Transform PhoneSettingsInfoPrefab;

    public List<string> appNames = new List<string>();
    private int currentIndex;

    // Use this for initialization
    void Start () {
        currentIndex = 0;
        var AppSettingsPanel = Instantiate(AppSettingsPanelPrefab);
        var PhoneSettingsInfo = Instantiate(PhoneSettingsInfoPrefab);
        AppSettingsPanel.SetParent(BaseCanvasPrefab, false);
        PhoneSettingsInfo.SetParent(BaseCanvasPrefab,false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
