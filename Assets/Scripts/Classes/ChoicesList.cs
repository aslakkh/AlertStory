using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChoicesList", menuName = "Alert/ChoicesList", order = 1)]
public class ChoicesList : ScriptableObject {

    public List<Choice> choicesList;

    public int GetCount()
    {
        return choicesList.Count;
    }
}
