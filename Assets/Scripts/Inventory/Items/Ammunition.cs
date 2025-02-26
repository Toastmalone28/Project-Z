using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Empty Ammunition", menuName = "Inventory System/Items/Ammunition")]
public class Ammunition : ItemObject
{
    public WeaponType ammoType;
}
