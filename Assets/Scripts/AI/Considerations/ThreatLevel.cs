using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThreatLevel", menuName = "UtilityAI/Considerations/ThreatLevel")]
public class ThreatLevel : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(CalculateThreatLevel(npc));
        return score;
    }

    private float CalculateThreatLevel(NPCController npc)
    {
        bool hasWeapon = npc.weaponHandler.currentWeapon != null;
        bool hasAmmo = false;
        bool lowHealth = false;
        bool hasHealingItem = npc.inventory.HasConsumable(ConsumableType.Healing);
        bool isZombie = npc.threatHandler.GetClosestInVision("Zombie") != null;
        int zombieCount = 0;
        bool isHuman = npc.threatHandler.GetClosestInVision("Human") != null;
        bool wasAggressive = npc.threatHandler.DetectedEnemies.Contains(npc.stats.recentlyAttackedBy);

        float threatScore = 0f;

        if (hasWeapon)
            hasAmmo = npc.inventory.HasAmmunition(npc.weaponHandler.currentWeaponData.weaponType) && npc.weaponHandler.CurrentAmmo > 0;

        if(npc.stats.health <= 30f)
            lowHealth = true;

        if (isZombie)
        {
            foreach (GameObject item in npc.threatHandler.DetectedEnemies)
            {
                if(item.CompareTag("Zombie"))
                    zombieCount++;
            }
        }

        if (!hasWeapon) threatScore += 0.2f;
        if (!hasAmmo) threatScore += 0.2f;
        if (lowHealth) threatScore += 0.3f;
        if (!hasHealingItem && lowHealth) threatScore += 0.1f;

        if (isZombie)
        {
            threatScore += 0.2f + 0.05f * Mathf.Clamp(zombieCount, 0, 10);
        }

        if (isHuman)
        {
            threatScore += wasAggressive ? 0.5f : 0.2f;
        }

        return Mathf.Clamp01(threatScore);
    }
}
