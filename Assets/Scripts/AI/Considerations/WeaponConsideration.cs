using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "UtilityAI/Considerations/Weapon")]
public class WeaponConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(npc.weaponHandler.currentWeapon != null));
        return score;
    }
}
