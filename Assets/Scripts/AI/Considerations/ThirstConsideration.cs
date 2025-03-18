using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thirst", menuName = "UtilityAI/Considerations/Thirst")]
public class ThirstConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.thirst / npc.stats.maxThirst));
        return score;
    }
}
