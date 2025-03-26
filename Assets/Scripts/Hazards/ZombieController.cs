using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState { Inactive, Active}
public class ZombieController : MonoBehaviour
{
    public float interestLossDistance;
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;

    private GameObject currentTarget;
    private ZombieState currentState;
    private NavMeshAgent agent;

    private float hitDelay;

    private void Awake()
    {
        currentState = ZombieState.Inactive;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Human") || currentTarget != null)
            return;

        currentTarget = other.gameObject;
        currentState = ZombieState.Active;

        agent.SetDestination(currentTarget.transform.position);
    }

    private void FixedUpdate()
    {
        hitDelay -= Time.deltaTime;

        if (currentState == ZombieState.Inactive)
            return;
        if (currentTarget != null)
            agent.SetDestination(currentTarget.transform.position);
        else
        {
            LoseInterest();
            return;
        }

        if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange)
        {
            agent.isStopped = true;
            AttackTarget();
        }
        else if(agent.isStopped)
            agent.isStopped = false;

        if (Vector3.Distance(transform.position, currentTarget.transform.position) >= interestLossDistance)
            LoseInterest();

    }

    private void LoseInterest()
    {
        agent.ResetPath();
        currentState = ZombieState.Inactive;
        currentTarget = null;
    }

    private void AttackTarget()
    {
        if(hitDelay <= 0)
        {
            hitDelay = attackSpeed;
            currentTarget.GetComponent<StatsHandler>().DealDamage(attackDamage);
        }
    }
}
