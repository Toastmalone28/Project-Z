using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class WeaponHandler : MonoBehaviour
{
    public Transform weaponHoldPoint;
    public Transform aimDirection;
    public LayerMask whatIsHittable;

    private GameObject currentWeapon;
    private Weapon currentWeaponData;
    private Transform barrelPoint;
    private SerializedDictionary<Weapon, int> ammoCache = new SerializedDictionary<Weapon, int>();
    private EventHandler eventHandler;

    private int currentAmmo;

    private void Awake()
    {
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        eventHandler = GetComponent<EventHandler>();
        eventHandler.OnWeaponChange += EquipWeapon;
        eventHandler.AmmoReturnEvent += AddAmmo;
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            SaveAmmo();
            Destroy(currentWeapon);
        }

        currentWeaponData = weapon;

        currentWeapon = Instantiate(weapon.itemPrefab, weaponHoldPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        if(!ammoCache.TryGetValue(weapon, out currentAmmo))
        {
            currentAmmo = weapon.maxAmmo;
            ammoCache[weapon] = currentAmmo;
        }

        barrelPoint = currentWeapon.transform.Find("BarrelPoint");

        Debug.Log(weapon.itemName + " equipped. Ammo: " + currentAmmo + "/" +  weapon.maxAmmo);
    }

    private void SaveAmmo()
    {
        if (currentWeaponData != null)
        {
            ammoCache[currentWeaponData] = currentAmmo;
        }
    }

    public void Shoot()
    {
        if(barrelPoint == null || aimDirection == null || currentWeaponData == null)
            return;

        if (currentAmmo == 0)
        {
            Debug.LogWarning("Weapon has no ammo");
            return;
        }
        Debug.DrawRay(barrelPoint.position, aimDirection.forward * 100f, Color.red, 1f);
        if (Physics.Raycast(barrelPoint.position, aimDirection.forward, out RaycastHit hit, currentWeaponData.range))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Missed");
        }
        currentAmmo--;
    }

    public void ReloadWeapon()
    {
        if (currentWeaponData == null || currentAmmo >= currentWeaponData.maxAmmo) 
            return;

        int neededAmmo = currentWeaponData.maxAmmo - currentAmmo;

        eventHandler.RequestAmmo(currentWeaponData, neededAmmo);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
    }
}
