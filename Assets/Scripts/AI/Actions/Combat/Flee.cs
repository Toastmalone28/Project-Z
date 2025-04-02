using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Flee", menuName ="UtilityAI/Actions/Flee")]
public class Flee : Action
{
    public override void Execute(NPCController npc)
    {
        npc.Flee();
    }
}
