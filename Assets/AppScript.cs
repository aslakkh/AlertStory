using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScript : MonoBehaviour {

    public string appName;
    public enum Settings { Friends, Private, Public};
    public List<string> settingDescription = new List<string>();
    public Dictionary<string,Settings> appSettings = new Dictionary<string,Settings >();
    public bool showSettings;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
