using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InvitationConsideration", menuName ="UtilityAI/Considerations/Invitation")]
public class InvitationConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;

    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(npc.groupHandler.isInvited));
        return score;
    }
}
