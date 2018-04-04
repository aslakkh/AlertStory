using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EventView : MonoBehaviour {

    public Image eventIllustration;
    public Text eventTitle;
    public Text eventDescription;


    public void SetEventTitle(string text)
    {
        eventTitle.text = text;
    }

    public void SetEventDescription(string text)
    {
        eventDescription.text = text;
    }


    
}
