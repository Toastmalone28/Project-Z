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

    internal void ChangeEquipment(float newArmor)
    {
        OnEquipmentChange.Invoke(newArmor);
    }

    internal void ChangeWeapon(Weapon currentWeapon)
    {
        OnWeaponChange.Invoke(currentWeapon);
    }

    internal void RequestAmmo(Weapon currentWeaponData, int neededAmmo)
    {
        AmmoRequestEvent.Invoke(currentWeaponData, neededAmmo);
    }

    internal void ReturnAmmo(int availableAmmo)
    {
        AmmoReturnEvent.Invoke(availableAmmo);
    }

    internal void ConsumeItem(ConsumableType type, int value)
    {
        OnConsumableUsed.Invoke(type, value);
    }
}
