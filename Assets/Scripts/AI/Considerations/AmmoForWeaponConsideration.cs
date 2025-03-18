using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoForWeapon", menuName = "UtilityAI/Considerations/AmmoForWeapon")]
public class AmmoForWeaponConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        Weapon currentWeapon = npc.weaponHandler.currentWeaponData;

        if(currentWeapon == null)
            return 0f;

        score = responseCurve.Evaluate(Convert.ToInt32(npc.inventory.HasAmmunition(currentWeapon.weaponType)));
        return score;
    }
}
