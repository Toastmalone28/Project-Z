using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoCount", menuName = "UtilityAI/Considerations/AmmoCount")]
public class AmmoCountConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        Weapon currentWeapon = npc.weaponHandler.currentWeaponData;

        if (currentWeapon == null)
            return 0f;

        score = responseCurve.Evaluate((float)npc.weaponHandler.CurrentAmmo / currentWeapon.maxAmmo * 100f);
        return score;
    }
}
