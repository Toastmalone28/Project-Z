using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThreatsDetected", menuName = "UtilityAI/Considerations/ThreatsDetected")]
public class ThreatsDetected : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.threatHandler.DetectedEnemies.Count);
        return score;
    }
}
