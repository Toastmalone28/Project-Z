using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchItems", menuName = "UtilityAI/Actions/SearchItems")]
public class SearchItems : Action
{
    public override void Execute(NPCController npc)
    {
        npc.SearchForItems();
    }
}
