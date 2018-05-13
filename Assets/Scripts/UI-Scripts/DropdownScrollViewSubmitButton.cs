using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownScrollViewSubmitButton : MonoBehaviour {

	public void OnClick()
    {
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
