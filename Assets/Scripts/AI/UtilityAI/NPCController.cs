using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public MovementController movementController;
    public AIBrain aiBrain;
    public List<Action> availableActions;
    public Inventory inventory;
    public WeaponHandler weaponHandler;
    public StatsHandler stats;
    public EventHandler eventHandler;
    public EquipmentHandler equipmentHandler;
    public SensingHandler sensingHandler;
    public POIHandler poiHandler;
    public PlayerTypeHandler playerTypeHandler;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        aiBrain = GetComponent<AIBrain>();
        inventory = GetComponent<Inventory>();
        weaponHandler = GetComponent<WeaponHandler>();
        stats = GetComponent<StatsHandler>();
        eventHandler = GetComponent<EventHandler>();
        equipmentHandler = GetComponent<EquipmentHandler>();
        sensingHandler = GetComponent<SensingHandler>();
        poiHandler = GetComponent<POIHandler>();
        playerTypeHandler = GetComponent<PlayerTypeHandler>();
    }
    private void Update()
    {
        if (aiBrain.finishedDeciding)
        {
            aiBrain.finishedDeciding = false;
            aiBrain.bestAction.Execute(this);
        }
    }

    public void OnFinishedAction()
    {
        aiBrain.DecideBestAction(availableActions);
    }

    #region Actions
    internal void Drink(int time)
    {
        StartCoroutine(DrinkCoroutine(time));
    }

    internal void Heal(int time)
    {
        StartCoroutine(HealCoroutine(time));
    }

    public void Eat(int time)
    {
        StartCoroutine(EatCoroutine(time));
    }
    internal void SearchForItems()
    {
        StartCoroutine(SearchItemsCoroutine());
    }
    public void EquipArmor(EquipmentType type)
    {
        StartCoroutine(EquipArmorCoroutine(1, type));
    }
    public void EquipWeapon(WeaponType type)
    {
        StartCoroutine(EquipWeaponCoroutine(1, type));
    }
    public void ReloadWeapon()
    {
        if (weaponHandler.currentWeaponData != null)
            StartCoroutine(ReloadWeaponCoroutine(weaponHandler.currentWeaponData.reloadTime));
        else
            Debug.Log(stats.Name + " tries to reload, but has no weapon equipped");
    }
    public void ExploreArea(PointOfInterest poi)
    {
        StartCoroutine(ExploreAreaCoroutine(poi, 2f));
    }
    public void MoveAroundArea()
    {
        StartCoroutine(MoveAroundAreaCoroutine(1f));
    }


    #endregion

    #region Coroutines
    private IEnumerator EatCoroutine(float time)
    {
        if (!inventory.HasConsumable(ConsumableType.Food))
            Debug.Log(stats.Name + " wants to eat, but has no food");
        else
        {
            yield return new WaitForSeconds(time);

            inventory.UseItem(ConsumableType.Food);

            Debug.Log("I just ate some food");
        }
        OnFinishedAction();
    }
    private IEnumerator DrinkCoroutine(float time)
    {
        if (!inventory.HasConsumable(ConsumableType.Water))
            Debug.Log(stats.Name + " wants to drink, but has no water");
        else
        {
            yield return new WaitForSeconds(time);

            inventory.UseItem(ConsumableType.Water);

            Debug.Log("I just drank some water");
        }
        OnFinishedAction();
    }
    private IEnumerator HealCoroutine(float time)
    {
        if (!inventory.HasConsumable(ConsumableType.Healing))
            Debug.Log(stats.Name + " wants to heal, but has no healing items");
        else
        {
            yield return new WaitForSeconds(time);

            inventory.UseItem(ConsumableType.Healing);

            Debug.Log("I just healed my injuries");
        }
        OnFinishedAction();
    }
    private IEnumerator SearchItemsCoroutine()
    {
        Debug.Log("I am searching for Items");

        if(sensingHandler.TargetsInView.Count > 0)
        {
            float distance = Mathf.Infinity;
            GameObject closestItem = null;

            foreach (GameObject item in sensingHandler.TargetsInView)
            {
                float tempDistance = Vector3.Distance(transform.position, item.transform.position);
                if (tempDistance < distance)
                {
                    closestItem = item;
                    distance = tempDistance;
                }
            }
            movementController.MoveTo(closestItem.transform.position);
        }


        yield return null;
        OnFinishedAction();
    }
    private IEnumerator EquipArmorCoroutine(int time, EquipmentType type)
    {
        if (!inventory.HasEquipment(type))
            Debug.Log(stats.Name + " wants to equip " + type.ToString() + " but doesn't have it in it's inventory");
        else
        {
            yield return new WaitForSeconds(time);

            equipmentHandler.EquipItem(type);

            Debug.Log(stats.Name + " equipped a " + type.ToString());
        }
        OnFinishedAction();
    }
    private IEnumerator EquipWeaponCoroutine(int time, WeaponType type)
    {
        Weapon weaponToEquip = inventory.HasWeapon(type);
        if (weaponToEquip == null)
            Debug.Log(stats.Name + " wants to equip " + type.ToString() + " but doesn't have it in it's inventory");
        else
        {
            yield return new WaitForSeconds(time);

            equipmentHandler.EquipItem(type);
        }
        OnFinishedAction();
    }
    private IEnumerator ReloadWeaponCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        weaponHandler.ReloadWeapon();

        OnFinishedAction();
    }
    private IEnumerator MoveAroundAreaCoroutine(float time)
    {
        Vector3 randomPosition = GetRandomPoint();
        if (randomPosition != Vector3.zero)
        {
            movementController.MoveTo(randomPosition);
            while (movementController.agent.pathPending || movementController.agent.remainingDistance > 0.5f)
            {
                yield return null;
            }
            yield return new WaitForSeconds(time);
        }
        OnFinishedAction();
    }
    private Vector3 GetRandomPoint()
    {
        PointOfInterest poi = poiHandler.currentArea;
        for(int i = 0; i < 10;  i++)
        {
            float randomX = Random.Range(poi.transform.position.x + poi.areaCenter.x - poi.areaSize.x / 2, poi.transform.position.x + poi.areaCenter.x + poi.areaSize.x / 2);
            float randomZ = Random.Range(poi.transform.position.z + poi.areaCenter.z - poi.areaSize.z / 2, poi.transform.position.z + poi.areaCenter.z + poi.areaSize.z / 2);
            float randomY = Random.Range(poi.transform.position.y + poi.areaCenter.y - poi.areaSize.y / 2, poi.transform.position.y + poi.areaCenter.y + poi.areaSize.y / 2);

            Vector3 randomPoint = new Vector3(randomX, randomY, randomZ);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, poi.areaSize.y / 2, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }

    private IEnumerator ExploreAreaCoroutine(PointOfInterest poi, float time)
    {
        if (poi != null)
            movementController.MoveTo(poi.transform.position);

        eventHandler.UpdateDestination(poi);

        while (movementController.agent.pathPending || movementController.agent.remainingDistance > 0.5f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(time);

        OnFinishedAction();
    }

    #endregion
}
