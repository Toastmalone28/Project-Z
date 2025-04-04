using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group invitation", menuName = "UtilityAI/Actions/Group invitation")]
public class GroupInvitationAction : Action
{
    public override void Execute(NPCController npc)
    {
        npc.InviteToGroup();
    }
}
