using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformativeEventView : EventView {
    public Text button;

    public void SetButtonText(string text)
    {
        button.text = text;
    }

}
