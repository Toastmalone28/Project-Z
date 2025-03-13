using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Eat", menuName ="UtilityAI/Actions/Eat")]
public class Eat : Action
{
    public override void Execute(NPCController npc)
    {
        npc.Eat(3);
    }
}
