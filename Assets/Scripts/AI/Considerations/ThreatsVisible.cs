using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThreatsVisible", menuName = "UtilityAI/Considerations/ThreatsVisible")]
public class ThreatsVisible : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.threatHandler.VisibleEnemies.Count);
        return score;
    }
}
