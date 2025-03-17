using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    public SerializedDictionary<EquipmentType, ItemObject> equipment = new SerializedDictionary<EquipmentType, ItemObject>();
    public Weapon currentWeapon;

    private EventHandler eventHandler;
    private Inventory inventory;

    private void Awake()
    {
        InitializeEquipment();
        InitializeEvents();
    }

    //Creates keys for each equipment type
    private void InitializeEquipment()
    {
        equipment.Clear();
        equipment.Add(EquipmentType.Head, null);
        equipment.Add(EquipmentType.Chest, null);
        equipment.Add(EquipmentType.Legs, null);
        equipment.Add(EquipmentType.Feet, null);

        inventory = GetComponent<Inventory>();
    }

    private void InitializeEvents()
    {
        eventHandler = GetComponent<EventHandler>();
        eventHandler.AmmoRequestEvent += HandleAmmoRequest;
    }
    //Loop through each item in the inventory to find an equipment item and equip it
    public void EquipItem(EquipmentType type)
    {
        foreach (ContainerSlot slot in inventory.slots)
        {
            if (slot.item.type != ItemType.Equipment)
                continue;

            Equipment itemToEquip = slot.item as Equipment;

            if (itemToEquip.equipType != type)
                continue;

            //if the designated equipment slot is not empty, change the inventory item with the equipped one
            if (equipment[itemToEquip.equipType] != null)
            {
                ItemObject temp = equipment[itemToEquip.equipType];
                equipment[itemToEquip.equipType] = itemToEquip;
                slot.item = temp;
                UpdateArmor();
                return;
            }
            else
            {
                equipment[itemToEquip.equipType] = itemToEquip;
                inventory.RemoveItem(itemToEquip, 1);
                UpdateArmor();
                return;
            }
        }
        Debug.LogWarning("No equipment found in inventory");
    }
    public void EquipItem(WeaponType type)
    {
        foreach (ContainerSlot slot in inventory.slots)
        {
            if (slot.item.type != ItemType.Weapon)
                continue;

            Weapon weaponToEquip = slot.item as Weapon;

            if (weaponToEquip.weaponType != type)
                continue;

            if (currentWeapon != null)
            {
                ItemObject temp = currentWeapon;
                currentWeapon = weaponToEquip;
                slot.item = temp;
                eventHandler.ChangeWeapon(currentWeapon);
                return;
            }
            else
            {
                currentWeapon = weaponToEquip;
                inventory.RemoveItem(weaponToEquip, 1);
                eventHandler.ChangeWeapon(currentWeapon);
                return;
            }
        }
    }

    //Add each armor level up and invoke event to add the new armor level to the player stats
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
        eventHandler.ChangeEquipment(newArmor);
    }
    //Listen to ammorequest event and look for specified ammo type + amount
    private void HandleAmmoRequest(Weapon requestedWeapon, int requestedAmount)
    {
        foreach (ContainerSlot slot in inventory.slots)
        {
            if (slot.item is Ammunition ammunition)
            {
                if (requestedWeapon.weaponType == ammunition.ammoType)
                {
                    int availableAmmo = inventory.CheckForAmmo(slot, requestedAmount);
                    eventHandler.ReturnAmmo(availableAmmo);
                    return;
                }
            }
        }
        Debug.Log("Required ammunition not available");
    }


}
