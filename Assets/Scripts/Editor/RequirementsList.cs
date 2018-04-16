using UnityEngine;
using System.Collections.Generic;

public class RequirementsList : ScriptableObject
{
    public List<Requirement> list;

    public void Init()
    {
        list = new List<Requirement>();
    }
}
