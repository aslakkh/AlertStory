using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownScrollViewSubmitButton : MonoBehaviour {
    private SoundManager soundManager;

    private void Awake() {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    
	public void OnClick()
    {
        soundManager.sfxSource.Play();
        try
        {
            //call next day
            GameObject.Find("GameManager").GetComponent<GameManager>().NextDay(batteryDepleted: false);
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e, this);
        }
        
    }
}
