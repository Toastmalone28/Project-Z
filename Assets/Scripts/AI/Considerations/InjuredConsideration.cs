using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Injured", menuName ="UtilityAI/Considerations/Injured")]
public class InjuredConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.health / npc.stats.maxHealth));
        return score;
    }
}
