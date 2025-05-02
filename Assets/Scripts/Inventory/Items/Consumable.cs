using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType { Healing, Food, Water}
[CreateAssetMenu(fileName = "New Empty Consumable", menuName = "Inventory System/Items/Consumable")]
public class Consumable : ItemObject
{
    public ConsumableType consumableType;
    public int value;
}
