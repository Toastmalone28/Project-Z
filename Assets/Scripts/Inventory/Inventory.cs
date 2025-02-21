using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ContainerSlot> slots = new List<ContainerSlot>();
    public int maxSlots = 20;

    public bool AddItem(Item newItem, int amount)
    {
        if (!newItem.itemData.isUnique && slots.Count > 0)
        {
            ContainerSlot existingSlot = slots.Find(slot => slot.item == newItem.itemData);
            if (existingSlot != null)
            {
                existingSlot.quantity += newItem.amount;
                return true;
            }
        }

        if (slots.Count < maxSlots)
        {
            ContainerSlot cs = new ContainerSlot(newItem.itemData, amount);

            slots.Add(cs);
            return true;
        }
        return false;
    }

    public bool RemoveItem(ItemObject item, int amount)
    {
        ContainerSlot slot = slots.Find(s => s.item.name == item.name);
        if (slot != null)
        {
            slot.quantity -= amount;
            if (slot.quantity <= 0) slots.Remove(slot);
            return true;
        }
        return false;
    }
}
