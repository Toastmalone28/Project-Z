using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAroundArea", menuName = "UtilityAI/Actions/MoveAroundArea")]
public class MoveAroundArea : Action
{
    public override void Execute(NPCController npc)
    {
        npc.MoveAroundArea();
    }
}
