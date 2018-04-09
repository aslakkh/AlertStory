using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformativeEventView : EventView {
    public Button button;

    private Text buttonText;

    private void Awake()
    {
        buttonText = button.gameObject.GetComponentInChildren<Text>();
    }

    public void SetButtonText(string text)
    {
        buttonText.text = text;
    }

}
