using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostView : MonoBehaviour {
    public Text title;
    public Text content;
    public Text date;

    public void SetTitle(string s)
    {
        title.text = s;
    }

    public void SetContent(string s)
    {
        content.text = s;
    }

    public void SetDate(string s)
    {
        date.text = s;
    }
	
}
