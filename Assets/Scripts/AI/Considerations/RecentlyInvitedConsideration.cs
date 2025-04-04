using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Recently Invited", menuName ="UtilityAI/Considerations/RecentlyInvited")]
public class RecentlyInvitedConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        if (npc.groupHandler.currentGroup == null)
            return npc.playerTypeHandler.playerTypeData.teamUp;
        score = responseCurve.Evaluate(Convert.ToInt32(npc.groupHandler.currentGroup.RecentlyInvited.Contains(npc.threatHandler.GetClosestInVision("Human"))));
        return score;
    }
}
