using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanInVision", menuName = "UtilityAI/Considerations/HumanInVision")]
public class HumanInVision : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(npc.threatHandler.GetClosestInVision("Human") != null));
        return score;
    }
}
