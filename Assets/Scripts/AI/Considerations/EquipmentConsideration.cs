using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "UtilityAI/Considerations/Equipment")]
public class EquipmentConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(CheckEquipment(npc));
        return score;
    }

    private float CheckEquipment(NPCController npc)
    {
        float equipmentScore = 0f;

        if (npc.equipmentHandler.equipment[EquipmentType.Head] == null)
            if (npc.inventory.HasEquipment(EquipmentType.Head))
                equipmentScore++;
        if (npc.equipmentHandler.equipment[EquipmentType.Chest] == null)
            if (npc.inventory.HasEquipment(EquipmentType.Chest))
                equipmentScore++;
        if (npc.equipmentHandler.equipment[EquipmentType.Legs] == null)
            if (npc.inventory.HasEquipment(EquipmentType.Legs))
                equipmentScore++;
        if (npc.equipmentHandler.equipment[EquipmentType.Feet] == null)
            if (npc.inventory.HasEquipment(EquipmentType.Feet))
                equipmentScore++;

        return equipmentScore;
    }
}
