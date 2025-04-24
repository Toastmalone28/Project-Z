using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BetterWeaponAvailable", menuName = "UtilityAI/Considerations/BetterWeaponAvailable")]
public class BetterWeaponAvailable : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(CheckForBetterWeapon(npc));
        return score;
    }

    private float CheckForBetterWeapon(NPCController npc)
    {
        Weapon weapon = npc.weaponHandler.currentWeaponData;

        if (weapon == null)
            return 1f;

        int ammo = npc.weaponHandler.CurrentAmmo;

        float currentScore = EvaluateWeaponUtility(weapon, ammo);

        foreach (Weapon w in npc.inventory.GetAllWeapons())
        {
            if (w == weapon)
                continue;

            int cachedAmmo = 0;
                if (npc.weaponHandler.ammoCache.ContainsKey(w))
            cachedAmmo = npc.weaponHandler.ammoCache[w];
                else
                cachedAmmo = w.maxAmmo;

            bool hasAmmo = cachedAmmo > 0 || npc.inventory.HasAmmunition(w.weaponType);

            if (!hasAmmo)
                continue;

            float weaponScore = EvaluateWeaponUtility(w, cachedAmmo);

            if (weaponScore > currentScore + 0.1f)
                return 1f;
        }
        return 0f;
    }

    private float EvaluateWeaponUtility(Weapon weapon, int ammoCount)
    {
        float wScore = 0f;

        if (ammoCount > 0)
            wScore += 1f;

        wScore += weapon.damage * 0.5f;
        wScore += weapon.range * 0.3f;

        return wScore;
    }
}
