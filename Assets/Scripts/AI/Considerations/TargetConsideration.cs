using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentTarget", menuName = "UtilityAI/Considerations/CurrentTarget")]
public class TargetConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(npc.threatHandler.currentTarget == null));
        return score;
    }
}
