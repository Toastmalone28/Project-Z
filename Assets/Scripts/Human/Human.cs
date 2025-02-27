using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public string Name;
    //public Team team;
    private Inventory inventory;
    private EventHandler eventHandler;

    public float maxHealth = 100f;
    public float maxStamina = 100f;
    public float maxHunger = 100f;
    public float maxThirst = 100f;

    private float health;
    private float stamina;
    private float hunger;
    private float thirst;
    private float armor = 0f;

    //TODO: add availableActions, brain and movementController

    private void Awake()
    {
        InitializeStats();
        InitializeEvents();
        StartCoroutine(GetHungry());
        StartCoroutine(GetThirsty());
    }

    private void InitializeEvents()
    {
        eventHandler = GetComponent<EventHandler>();
        eventHandler.OnEquipmentChange += UpdateArmor;
        eventHandler.OnConsumableUsed += RestoreStats;
    }

    public void DealDamage(float damage)
    {
        //Calculate damage by multiplying damage by 100 divided by 100 + current armor
        float newDamage = damage * 100 / (100 + armor);
        health = health - newDamage;
    }

    private void InitializeStats()
    {
        health = maxHealth;
        stamina = maxStamina;
        hunger = maxHunger;
        thirst = maxThirst;
    }

    private void UpdateArmor(float amount)
    {
        armor = amount;
        Debug.Log(name + ": " + armor);
    }

    private IEnumerator GetHungry()
    {
        float depletionRate = 0.1f;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(hunger > 0.01f)
                hunger -= depletionRate;
            else if(stamina > 0.01f)
                stamina -= depletionRate;
            else
                health -= depletionRate;
        }
    }
    private IEnumerator GetThirsty()
    {
        float depletionRate = 0.2f;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(thirst > 0.01f)
                thirst -= depletionRate;
            else if(stamina > 0.01f)
                stamina -= depletionRate;
            else
                health -= depletionRate;
        }
    }
    private void RestoreStats(ConsumableType type, int amount)
    {
        switch (type)
        {
            case ConsumableType.Healing:
                RestoreHealth(amount);
                break;
            case ConsumableType.Food:
                RestoreHunger(amount);
                break;
            case ConsumableType.Water:
                RestoreThirst(amount);
                break;
        }
    }


    private void RestoreHealth(int amount)
    {
        if (health >= maxHealth)
            return;

        if (health + amount >= maxHealth)
            health = maxHealth;
        else
            health += amount;
    }

    private void RestoreHunger(int amount)
    {
        if (hunger + amount >= maxHunger)
            hunger = maxHunger;
        else
            hunger += amount;
    }

    private void RestoreThirst(int amount)
    {
        if (thirst + amount >= maxThirst)
            thirst = maxThirst;
        else
            thirst += amount;
    }
}
