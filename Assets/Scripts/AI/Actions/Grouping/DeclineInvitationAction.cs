using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decline invitation", menuName = "UtilityAI/Actions/Decline invitation")]
public class DeclineInvitationAction : Action
{
    public override void Execute(NPCController npc)
    {
        npc.DeclineInvitation();
    }
}
