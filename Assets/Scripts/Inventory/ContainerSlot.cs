using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContainerSlot
{
    public ItemObject item;
    public int quantity;

    public ContainerSlot(ItemObject newItem, int amount)
    {
        item = newItem;
        quantity = amount;
    }
}
