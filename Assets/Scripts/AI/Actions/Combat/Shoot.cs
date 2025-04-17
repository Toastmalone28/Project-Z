using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public enum TargetTag { Human, Zombie}
[CreateAssetMenu(fileName = "Shoot", menuName = "UtilityAI/Actions/Shoot")]
public class Shoot : Action
{
    public TargetTag targetTag;
    public override void Execute(NPCController npc)
    {
        npc.Shoot(targetTag.ToString());
    }
}
