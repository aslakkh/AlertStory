using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostView : MonoBehaviour {
    public Text title;
    public Text content;
    public Text date;
    private List<string> currentDay;

    private void Start()
    {
        currentDay = GameManager.Instance.informationPackage;
    }


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

    public void OnPostClicked()
    {
        if(!currentDay.Contains(content.text) && !content.text.Equals(""))
        {
            currentDay.Add("Posts");
            currentDay.Add(content.text.ToString());
        }
    }
}
