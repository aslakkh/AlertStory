using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Resources;
using System.Xml;
using System.Xml.Serialization;

public class AppManager : MonoBehaviour {
    public List<string> appNamez = new List<string>();
    public GameObject App;
    public IEnumerable<string> appNames;

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        appNamez.Add("Facebook");
        appNamez.Add("Snapchat");
        appNamez.Add("1881");
        appNamez.Add("Strava");
        appNamez.Add("Instaskjegg");
        GameManager gm = GetComponentInParent<GameManager>();

        Debug.Log(gm.apps);
        foreach(string x in appNamez)
        {
            var newApp = Instantiate(App,this.transform) ;
            AppScript appscpt = newApp.GetComponent<AppScript>();
            appscpt.appName = x;
            // TODO: Add XML import of each app's settings as dict.
            // appscpt.appSettings = 
            
        }
        Debug.Log("Entered start method and this game was all I found:" + gm);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
