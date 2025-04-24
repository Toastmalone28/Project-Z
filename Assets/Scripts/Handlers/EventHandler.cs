using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public event Action<Weapon, int> AmmoRequestEvent;
    public event Action<int> AmmoReturnEvent;
    public event Action<float> OnEquipmentChange;
    public event Action<Weapon> OnWeaponChange;
    public event Action<ConsumableType, int> OnConsumableUsed;
    public event Action<PointOfInterest> UpdateCurrentAreaEvent;
    public event Action<PointOfInterest> UpdateDestinationEvent;
    public event Action<NPCController> OnPlayerDeathEvent;
    public event Action<Group> OnGroupUpdateEvent;
    public event Action<bool> OnMovementStateChange;

    internal void ChangeEquipment(float newArmor)
    {
        OnEquipmentChange?.Invoke(newArmor);
    }

    internal void ChangeWeapon(Weapon currentWeapon)
    {
        OnWeaponChange?.Invoke(currentWeapon);
    }

    internal void RequestAmmo(Weapon currentWeaponData, int neededAmmo)
    {
        AmmoRequestEvent?.Invoke(currentWeaponData, neededAmmo);
    }

    internal void ReturnAmmo(int availableAmmo)
    {
        AmmoReturnEvent?.Invoke(availableAmmo);
    }

    internal void ConsumeItem(ConsumableType type, int value)
    {
        OnConsumableUsed?.Invoke(type, value);
    }

    internal void UpdateCurrentArea(PointOfInterest pointOfInterest)
    {
        UpdateCurrentAreaEvent?.Invoke(pointOfInterest);
    }

    internal void UpdateDestination(PointOfInterest poi)
    {
        UpdateDestinationEvent?.Invoke(poi);
    }

    internal void PlayerDeath(NPCController npc)
    {
        OnPlayerDeathEvent?.Invoke(npc);
    }

    internal void GroupUpdate(Group currentGroup)
    {
        OnGroupUpdateEvent?.Invoke(currentGroup);
    }
    internal void ChangeMovementState(bool isMoving)
    {
        OnMovementStateChange?.Invoke(isMoving);
    }
}
