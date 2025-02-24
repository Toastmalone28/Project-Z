using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Head, Chest, Legs, Feet}
[CreateAssetMenu(fileName = "New Empty Equipment Item", menuName ="Inventory System/Items/Equipment")]
public class Equipment : ItemObject
{
    public EquipmentType equipType;

    public float armorLevel;
    public override void UseItem()
    {

    }
}
