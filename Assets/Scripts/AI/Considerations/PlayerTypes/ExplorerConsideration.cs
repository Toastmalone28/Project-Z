using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exploring", menuName = "UtilityAI/Considerations/PlayerTypes/Exploring")]
public class ExplorerConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.playerTypeHandler.playerTypeData.completeQuests);
        return score;
    }
}