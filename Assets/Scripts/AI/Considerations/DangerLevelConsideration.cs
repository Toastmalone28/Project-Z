using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DangerLevel", menuName = "UtilityAI/Considerations/DangerLevel")]
public class DangerLevelConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {

        return score;
    }
}
