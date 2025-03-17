using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "UtilityAI/Considerations/EquipmentInInventory")]
public class EquipmentInInventoryConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    [SerializeField] public EquipmentType type;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(CheckEquipment(npc));
        return score;
    }

    private float CheckEquipment(NPCController npc)
    {
        if (!npc.inventory.HasEquipment(type))
            return 0f;
        return 1f;
    }
}
