using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "ItemInInventory", menuName = "UtilityAI/Considerations/ItemInInventory")]
public class ItemInInventoryConsideration : Consideration
{
    [SerializeField] public AnimationCurve responseCurve;

    public ItemType itemType;

    public WeaponType weaponType;
    public ConsumableType consumableType;
    public EquipmentType equipmentType;
    public ResourceType resourceType;

    public override float ScoreConsideration(NPCController npc)
    {
        switch (itemType)
        {
            case ItemType.Weapon:
                score = responseCurve.Evaluate(Convert.ToInt32(npc.inventory.HasWeapon(weaponType)));
                break;
            case ItemType.Consumable:
                score = responseCurve.Evaluate(Convert.ToInt32(npc.inventory.HasConsumable(consumableType)));
                break;
            case ItemType.Equipment:
                score = responseCurve.Evaluate(Convert.ToInt32(npc.inventory.HasEquipment(equipmentType)));
                break;
            case ItemType.Resource:
                score = responseCurve.Evaluate(Convert.ToInt32(npc.inventory.HasResource(resourceType)));
                break;
            case ItemType.Ammunition:
                score = responseCurve.Evaluate(Convert.ToInt32(npc.inventory.HasAmmunition(weaponType)));
                break;
        }
        return score;
    }
}
