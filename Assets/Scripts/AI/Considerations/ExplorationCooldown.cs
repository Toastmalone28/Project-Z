using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ExplorationCooldown", menuName ="UtilityAI/Considerations/ExplorationCooldown")]
public class ExplorationCooldown : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;
    public override float ScoreConsideration(NPCController npc)
    {
        score = responseCurve.Evaluate(npc.poiHandler.explorationCooldown / 100f);
        return score;
    }
}
