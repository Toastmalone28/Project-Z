using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Killing", menuName = "UtilityAI/Considerations/PlayerTypes/Killing")]
public class KillerConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.playerTypeHandler.playerTypeData.fight);
        return score;
    }
}
