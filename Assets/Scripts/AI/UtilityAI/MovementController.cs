using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    public NavMeshAgent agent {  get; private set; }
    private EventHandler eventHandler;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        eventHandler = gameObject.GetComponent<EventHandler>();

        eventHandler.OnMovementStateChange += ChangeMovementState;
    }
    private void Update()
    {
        //agent.destination = transform.position + Vector3.forward;
    }

    public void MoveTo(Vector3 position)
    {
        eventHandler.ChangeMovementState(true);

        agent.destination = position;
    }
    private void ChangeMovementState(bool isMoving)
    {
        agent.isStopped = !isMoving;
    }
}
