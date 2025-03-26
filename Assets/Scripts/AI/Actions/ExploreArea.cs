using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explore", menuName = "UtilityAI/Actions/Explore")]
public class ExploreArea : Action
{
    public POIType type;
    public override void Execute(NPCController npc)
    {
        PointOfInterest nearestArea = POIManager.instance.GetClosestArea(npc, type);

        npc.ExploreArea(nearestArea);
    }
}
