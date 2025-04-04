using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accept invitation", menuName = "UtilityAI/Actions/Accept invitation")]
public class AcceptInvitationAction : Action
{
    public override void Execute(NPCController npc)
    {
        npc.AcceptInvitation();
    }
}
