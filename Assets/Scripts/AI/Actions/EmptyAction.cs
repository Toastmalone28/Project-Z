using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugAction", menuName = "UtilityAI/Actions/Debug")]
public class EmptyAction : Action
{
    public override void Execute(NPCController npc)
    {
        npc.OnFinishedAction();
    }
}
