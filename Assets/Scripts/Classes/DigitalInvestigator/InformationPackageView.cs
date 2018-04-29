using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPackageView : MonoBehaviour {
    
    public Text title;
    public List<string> informationList = new List<string>();
    public GameObject currentView;
    public GameObject informationPackagePrefab;
    public GameObject rect;

    public void displayInformationPackage() {
        if (currentView != null) {
            GameObject.Destroy(currentView);
        } 
        currentView = Instantiate(informationPackagePrefab);
        currentView.transform.SetParent(rect.transform, false);

    }

}
