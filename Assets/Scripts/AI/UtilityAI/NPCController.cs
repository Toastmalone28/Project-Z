using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(sensingHandler.targetsInView.Count > 0 && sensingHandler.targetsInView[0].gameObject.layer == sensingHandler.itemLayer)
            movementController.MoveTo(sensingHandler.targetsInView[0].transform.position);

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

    #endregion
}
