using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Hunger", menuName ="UtilityAI/Considerations/Hunger")]
public class HungerConsideration : Consideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(npc.stats.hunger / npc.stats.maxHunger));
        return score;
    }
}
