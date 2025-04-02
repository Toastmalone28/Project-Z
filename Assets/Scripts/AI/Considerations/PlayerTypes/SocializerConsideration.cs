using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Socializing", menuName = "UtilityAI/Considerations/PlayerTypes/Socializing")]
public class SocializerConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.playerTypeHandler.playerTypeData.teamUp);
        return score;
    }
}
