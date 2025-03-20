using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensingHandler : MonoBehaviour
{
    public float visionRange;
    public float visionAngle;
    public LayerMask itemLayer;
    public LayerMask hittableLayer;
    public LayerMask obstacleLayer;

    public List<GameObject> targetsInView { get; private set; }
    private void Awake()
    {
        targetsInView = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        ScanEnvironment();
    }
    public List<GameObject> ScanEnvironment()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, visionRange, itemLayer | hittableLayer);

        GetTargetsInView(colliders);

        return targetsInView;
    }

    private void GetTargetsInView(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.transform.IsChildOf(transform))
                continue;

            Vector3 directionToTarget = (collider.transform.position - transform.position).normalized;

            if (IsInView(collider.gameObject) && !targetsInView.Contains(collider.gameObject))
            {
                targetsInView.Add(collider.gameObject);
            }
            else if(targetsInView.Contains(collider.gameObject))
            {
                targetsInView.Remove(collider.gameObject);
            }
        }
    }

    private bool IsInView(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToTarget) > visionAngle / 2)
            return false;

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        return !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer);
    }

    private void HandleDetection(GameObject gameObject)
    {
        if(!targetsInView.Contains(gameObject))
            targetsInView.Add(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
