using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "UtilityAI/Actions/Chase")]
public class Chase : Action
{
    public override void Execute(NPCController npc)
    {
        npc.ChaseTarget();
    }
}
