using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "UtilityAI/Considerations/Armor")]
public class ArmorConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = Mathf.Clamp01(npc.stats.armor / 100);
        return score;
    }
}
