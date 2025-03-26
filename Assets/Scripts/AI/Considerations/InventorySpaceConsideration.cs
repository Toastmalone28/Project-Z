using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySpace", menuName = "UtilityAI/Considerations/InventorySpace")]
public class InventorySpaceConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.inventory.slots.Count * 1f / npc.inventory.maxSlots));
        return score;
    }
}
