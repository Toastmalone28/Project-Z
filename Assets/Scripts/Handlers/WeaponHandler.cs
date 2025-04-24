using AYellowpaper.SerializedCollections;
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
    [SerializeField] public SerializedDictionary<Weapon, int> ammoCache = new SerializedDictionary<Weapon, int>();
    private EventHandler eventHandler;

    private int currentAmmo;
    public int CurrentAmmo
    {
        get { return currentAmmo; }
        private set { currentAmmo = value; }
    }

    private float shotTimer;
    private bool isAiming;
    private Vector3 aimPosition = new Vector3(-0.4f, 0, 0.3f);

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
        eventHandler.OnMovementStateChange += ResetAim;
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

        if (!ammoCache.TryGetValue(weapon, out currentAmmo))
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

        if (!isAiming)
            AimWeapon();

        if (shotTimer < 0)
        {
            shotTimer = currentWeaponData.shotCooldown;

            Vector3 shotOrigin = barrelPoint.position;

            Vector3 direction = barrelPoint.TransformDirection(CalculateWeaponSpread());

            Debug.DrawRay(shotOrigin, direction.normalized * 100f, Color.red, 0.1f);

            if (Physics.Raycast(shotOrigin, direction.normalized, out RaycastHit hit, (int)currentWeaponData.range, whatIsHittable, QueryTriggerInteraction.Ignore))
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
    private Vector3 CalculateWeaponSpread()
    {
        return new Vector3(Random.Range(-currentWeaponData.spread, currentWeaponData.spread), Random.Range(-currentWeaponData.spread, currentWeaponData.spread), 1);
    }
    private void AimWeapon()
    {
        isAiming = true;
        currentWeapon.transform.localPosition = aimPosition;
    }
    private void ResetAim(bool isMoving)
    {
        if (isMoving && isAiming)
        {
            isAiming = false;
            currentWeapon.transform.localPosition -= aimPosition;
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

    public Weapon GetBestAvailableWeapon()
    {
        NPCController npc = GetComponent<NPCController>();
        Weapon best = null;
        float bestScore = 0f;

        foreach (Weapon weapon in npc.inventory.GetAllWeapons())
        {
            float score = EvaluateWeaponUtility(weapon, npc);

            if (score > bestScore)
            {
                bestScore = score;
                best = weapon;
            }
        }

        return best;
    }

    private float EvaluateWeaponUtility(Weapon weapon, NPCController npc)
    {
        float score = 0f;

        if (npc.inventory.HasAmmunition(weapon.weaponType))
            score += 1f;

        score += weapon.damage * 0.5f;
        score += weapon.range * 0.3f;

        return score;
    }
}
