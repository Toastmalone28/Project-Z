using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    public NavMeshAgent agent {  get; private set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //agent.destination = transform.position + Vector3.forward;
    }

    public void MoveTo(Vector3 position)
    {
        agent.destination = position;
    }
}
