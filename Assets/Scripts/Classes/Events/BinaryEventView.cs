using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryEventView : EventView {
    public Text affirmativeButton;
    public Text dissentiveButton;

    public void SetAffirmativeButtonText(string text)
    {
        affirmativeButton.text = text;
    }

    public void SetDissentiveButtonText(string text)
    {
        dissentiveButton.text = text;
    }
}
