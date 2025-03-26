using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentArea", menuName = "UtilityAI/Considerations/CurrentArea")]
public class CurrentAreaConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(npc.poiHandler.currentArea != null));
        return score;
    }
}
