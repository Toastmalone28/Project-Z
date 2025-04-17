using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThreatsVisible", menuName = "UtilityAI/Considerations/ThreatsVisible")]
public class ThreatsVisible : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(npc.threatHandler.GetClosestInVision("Zombie") != null));
        return score;
    }
}
