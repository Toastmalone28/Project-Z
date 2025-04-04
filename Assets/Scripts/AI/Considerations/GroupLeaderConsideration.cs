using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Group Leader", menuName ="UtilityAI/Considerations/GroupLeader")]
public class GroupLeaderConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Convert.ToInt32(EvaluateGroupStatus(npc)));
        return score;
    }

    private bool EvaluateGroupStatus(NPCController npc)
    {
        bool isGroupLeader = false;

        if(npc.groupHandler.currentGroup == null)
            return true;

        if(npc.groupHandler.currentGroup.groupLeader == npc)
            isGroupLeader = true;

        return isGroupLeader;
    }
}
