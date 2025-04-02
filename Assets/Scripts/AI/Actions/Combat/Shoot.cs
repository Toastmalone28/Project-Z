using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot", menuName = "UtilityAI/Actions/Shoot")]
public class Shoot : Action
{
    public override void Execute(NPCController npc)
    {
        npc.Shoot();
    }
}
