﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryEventView : EventView {
    public Button affirmativeButton;
    public Button dissentiveButton;

    private Text affirmativeButtonText;
    private Text dissentiveButtonText;

    private void Awake()
    {
        affirmativeButtonText = affirmativeButton.gameObject.GetComponentInChildren<Text>();
        dissentiveButtonText = dissentiveButton.gameObject.GetComponentInChildren<Text>();
    }

    public void SetAffirmativeButtonText(string text)
    {
        affirmativeButtonText.text = text;
    }

    public void SetDissentiveButtonText(string text)
    {
        dissentiveButtonText.text = text;
    }

}
