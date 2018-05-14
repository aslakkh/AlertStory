using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Change Objective Function")]
public class ChangeObjectiveFunction : FunctionCall {

    public List<Objective> objectivesList;
    private Dictionary<int, List<Objective>> objectives;
    public int targetDay;

    public override void triggerFunction()
    {
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        objectives = gm.objectives;
        if ( targetDay != 0 && objectives.ContainsKey(targetDay))
        {
            objectives[targetDay] = objectivesList;
            gm.objectives = objectives; // not sure if needed but better safe than buggy.
        }
    }
}
