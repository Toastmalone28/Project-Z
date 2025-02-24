using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;

public class Inventory : MonoBehaviour
{
    public List<ContainerSlot> slots = new List<ContainerSlot>();
    public int maxSlots = 20;

    public SerializedDictionary<EquipmentType, ItemObject> equipment = new SerializedDictionary<EquipmentType, ItemObject>();
    public Weapon currentWeapon;

    public event Action<float> OnEquipmentChange;

    private void Awake()
    {
        InitializeEquipment();
    }

    private void InitializeEquipment()
    {
        equipment.Clear();
        equipment.Add(EquipmentType.Head, null);
        equipment.Add(EquipmentType.Chest, null);
        equipment.Add(EquipmentType.Legs, null);
        equipment.Add(EquipmentType.Feet, null);
    }

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

    public void EquipItem()
    {
        foreach (ContainerSlot slot in slots)
        {
            if (slot.item.type == ItemType.Equipment)
            {
                Equipment newEquipment = (Equipment)slot.item;

                if(equipment[newEquipment.equipType] != null)
                {
                    ItemObject temp = equipment[newEquipment.equipType];
                    equipment[newEquipment.equipType] = newEquipment;
                    slot.item = temp;
                    UpdateArmor();
                    return;
                }
                else
                {
                    equipment[newEquipment.equipType] = newEquipment;
                    RemoveItem(newEquipment, 1);
                    UpdateArmor();
                    return;
                }

            }
        }
        Debug.LogWarning("No equipment found in inventory");
    }
    public void UpdateArmor()
    {
        float newArmor = 0f;
        foreach (KeyValuePair<EquipmentType, ItemObject> entry in equipment)
        {
            if (entry.Value is Equipment equipmentItem)
            {
                newArmor += equipmentItem.armorLevel;
            }
        }
        OnEquipmentChange?.Invoke(newArmor);
    }
}
