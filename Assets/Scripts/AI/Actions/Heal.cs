using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "UtilityAI/Actions/Heal")]
public class Heal : Action
{
    public override void Execute(NPCController npc)
    {
        npc.Heal(3);
    }
}