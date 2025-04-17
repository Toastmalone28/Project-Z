using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberOfUnexploredAreas", menuName = "UtilityAI/Considerations/UnexploredAreas")]
public class NumberUnexploredAreasConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(CalculateAreaPercentage(npc));
        return score;
    }

    private float CalculateAreaPercentage(NPCController npc)
    {
        float value = 0f;

        value = (float)POIManager.instance.availableAreas.Count / 100f * POIManager.instance.GetNumberOfUnexploredAreas(npc);

        return value;
    }
}
