using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achieving", menuName = "UtilityAI/Considerations/PlayerTypes/Achieving")]
public class AchieverConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.playerTypeHandler.playerTypeData.collectItems);
        return score;
    }
}
