using UnityEngine;
using System.Collections.Generic;

public class ChoiceList: ScriptableObject {
    public List<Choice> list;

    public Choice this[int itemIndex] {
        get { return list[itemIndex]; }
    }
}
