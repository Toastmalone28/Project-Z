using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "UtilityAI/Actions/Drink")]
public class Drink : Action
{
    public override void Execute(NPCController npc)
    {
        npc.Drink(3);
    }
}
