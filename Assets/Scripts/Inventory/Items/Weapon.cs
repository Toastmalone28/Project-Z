using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Light, Medium, Heavy}
[CreateAssetMenu(fileName="New Empty Weapon", menuName ="Inventory System/Items/Weapon")]
public class Weapon : ItemObject
{
    public GameObject weaponPrefab;
    public WeaponType weaponType;
    public float range;
    public int maxAmmo;
    public float reloadTime;
    public float shotCooldown;
    public float damage;
}
