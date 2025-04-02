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

    public float shotOffset;

    public GameObject currentWeapon {  get; private set; }
    public Weapon currentWeaponData { get; private set; }
    private Transform barrelPoint;
    private SerializedDictionary<Weapon, int> ammoCache = new SerializedDictionary<Weapon, int>();
    private EventHandler eventHandler;

    private int currentAmmo;
    public int CurrentAmmo
    {
        get { return currentAmmo; }
        private set { currentAmmo = value; }
    }

    private float shotTimer;

    private void Awake()
    {
        InitializeEvents();
    }

    private void FixedUpdate()
    {
        shotTimer -= Time.deltaTime;
    }

    private void InitializeEvents()
    {
        eventHandler = GetComponent<EventHandler>();
        eventHandler.OnWeaponChange += EquipWeapon;
        eventHandler.AmmoReturnEvent += AddAmmo;
    }

    private void EquipWeapon(Weapon weapon)
    {
        shotTimer = weapon.shotCooldown;

        if (weapon != null)
        {
            SaveAmmo();
            Destroy(currentWeapon);
        }

        currentWeaponData = weapon;

        currentWeapon = Instantiate(weapon.weaponPrefab, weaponHoldPoint);
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
        if (barrelPoint == null || aimDirection == null || currentWeaponData == null)
            return;

        if (currentAmmo == 0)
        {
            Debug.LogWarning("Weapon has no ammo");
            return;
        }

        if (shotTimer < 0)
        {
            shotTimer = currentWeaponData.shotCooldown;

            // Adjust shot position instead of direction
            Vector3 shotOrigin = barrelPoint.position + barrelPoint.right * shotOffset;

            // Debugging ray to check direction
            Debug.DrawRay(shotOrigin, aimDirection.forward * 100f, Color.red, 1f);

            // Ensure proper raycasting
            if (Physics.Raycast(shotOrigin, aimDirection.forward, out RaycastHit hit, (int)currentWeaponData.range, whatIsHittable, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("Hit: " + hit.collider.name);

                if (hit.collider.CompareTag("Zombie"))
                    hit.collider.GetComponent<ZombieController>().DealDamage(currentWeaponData.damage);
                if (hit.collider.CompareTag("Human") || hit.collider.CompareTag("Debug"))
                    hit.collider.GetComponent<StatsHandler>().DealDamage(currentWeaponData.damage, gameObject);
            }
            else
            {
                Debug.Log("Missed");
            }

            currentAmmo--;
        }
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
