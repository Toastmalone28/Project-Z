using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberOfUnexploredAreas", menuName = "UtilityAI/Considerations/UnexploredAreas")]
public class NumberUnexploredAreasConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(POIManager.instance.GetNumberOfUnexploredAreas(npc));
        return score;
    }
}
