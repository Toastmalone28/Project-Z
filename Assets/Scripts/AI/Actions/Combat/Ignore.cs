using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ignore", menuName = "UtilityAI/Actions/Ignore")]
public class Ignore : Action
{
    public override void Execute(NPCController npc)
    {
        npc.OnFinishedAction();
    }
}
