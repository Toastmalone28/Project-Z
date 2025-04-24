using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Switch Weapon", menuName = "UtilityAI/Actions/SwitchWeapon")]
public class SwitchWeapon : Action
{
    public override void Execute(NPCController npc)
    {
        Weapon bestWeapon = npc.weaponHandler.GetBestAvailableWeapon();

        if (bestWeapon != null)
        {
            npc.EquipWeapon(bestWeapon.weaponType);
        }
    }
}
