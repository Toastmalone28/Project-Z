using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipArmor", menuName = "UtilityAI/Actions/Equipment/Armor")]
public class EquipArmor : Action
{
    public EquipmentType type;
    public override void Execute(NPCController npc)
    {
        npc.EquipArmor(type);
    }
}
