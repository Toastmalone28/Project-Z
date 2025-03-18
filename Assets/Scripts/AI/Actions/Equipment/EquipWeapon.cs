using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipWeapon", menuName = "UtilityAI/Actions/Equipment/Weapon")]
public class EquipWeapon : Action
{
    public WeaponType type;
    public override void Execute(NPCController npc)
    {
        npc.EquipWeapon(type);
    }
}
