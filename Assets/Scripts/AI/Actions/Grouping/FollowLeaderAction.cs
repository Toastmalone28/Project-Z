using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow Leader", menuName = "UtilityAI/Actions/FollowLeader")]
public class FollowLeaderAction : Action
{
    public override void Execute(NPCController npc)
    {
        npc.FollowLeader();
    }
}
