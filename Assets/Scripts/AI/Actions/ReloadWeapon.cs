using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReloadWeapon", menuName = "UtilityAI/Actions/ReloadWeapon")]
public class ReloadWeapon : Action
{
    public override void Execute(NPCController npc)
    {
        npc.ReloadWeapon();
    }
}
