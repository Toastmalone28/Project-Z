using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensingHandler : MonoBehaviour
{
    public float visionRange;
    public float visionAngle;

    public float scanFrequency;
    private float scanInterval;
    private float scanTimer;

    public LayerMask itemLayer;
    public LayerMask obstacleLayer;

    public List<GameObject> TargetsInView
    {
        get
        {
            targetsInView.RemoveAll(obj => !obj);
            return targetsInView;
        }
    }
    private List<GameObject> targetsInView;
    private void Awake()
    {
        targetsInView = new List<GameObject>();
        scanInterval = 1f / scanFrequency;
    }

    private void FixedUpdate()
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanInterval;
            ScanEnvironment();
        }
    }
    public List<GameObject> ScanEnvironment()
    {
        targetsInView.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, visionRange, itemLayer);

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
