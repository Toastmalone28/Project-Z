using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;

public class Inventory : MonoBehaviour
{
    public List<ContainerSlot> slots;
    public int maxSlots = 20;

    public SerializedDictionary<EquipmentType, ItemObject> equipment = new SerializedDictionary<EquipmentType, ItemObject>();
    public Weapon currentWeapon;

    public event Action<float> OnEquipmentChange;
    public event Action<Weapon> OnWeaponChange;
    public event Action<int> AmmoReturnEvent;

    private void Awake()
    {
        InitializeEquipment();
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        if(TryGetComponent<WeaponHandler>(out WeaponHandler weaponHandler))
        {
            weaponHandler.AmmoRequestEvent += HandleAmmoRequest;
        }
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

            if(slot.item.type == ItemType.Weapon)
            {
                Weapon newWeapon = (Weapon)slot.item;

                if (currentWeapon != null)
                {
                    ItemObject temp = currentWeapon;
                    currentWeapon = newWeapon;
                    slot.item = temp;
                    OnWeaponChange.Invoke(currentWeapon);
                    return;
                }
                else
                {
                    currentWeapon = newWeapon;
                    RemoveItem(newWeapon, 1);
                    OnWeaponChange.Invoke(currentWeapon);
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

    private void HandleAmmoRequest(Weapon requestedWeapon, int requestedAmount)
    {
        foreach (ContainerSlot slot in slots)
        {
            if(slot.item is Ammunition ammunition)
            {
                if(requestedWeapon.weaponType == ammunition.ammoType)
                {
                    int availableAmmo = CheckForAmmo(slot, requestedAmount);
                    AmmoReturnEvent.Invoke(availableAmmo);
                    return;
                }
            }
        }
        Debug.Log("Required ammunition not available");
    }

    private int CheckForAmmo(ContainerSlot slot, int requestedAmount)
    {
        if(slot.quantity >= requestedAmount)
        {
            slot.quantity -= requestedAmount;

            if (slot.quantity <= 0)
                slots.Remove(slot);

            return requestedAmount;
        }
        else
        {
            int remainingAmmo = slot.quantity;

            slots.Remove(slot);

            return remainingAmmo;
        }
            
    }
}
