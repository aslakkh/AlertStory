using UnityEngine;

[System.Serializable]
public class Requirement
{
    public string requirementName;
    //Add more vars if needed

    public Requirement() { }

    public Requirement(string name)
    {
        this.requirementName = name;
    }
}
