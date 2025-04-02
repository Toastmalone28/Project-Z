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

    public List<GameObject> ItemsInView
    {
        get
        {
            itemsInView.RemoveAll(obj => !obj);
            return itemsInView;
        }
    }
    private List<GameObject> itemsInView;


    private void Awake()
    {
        itemsInView = new List<GameObject>();
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
    public void ScanEnvironment()
    {
        itemsInView.Clear();

        Collider[] itemColliders = Physics.OverlapSphere(transform.position, visionRange, itemLayer);

        GetTargetsInView(itemColliders, itemsInView);
    }

    private void GetTargetsInView(Collider[] colliders, List<GameObject> targetList)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.transform.IsChildOf(transform))
                continue;

            if (IsInView(collider.gameObject) && !targetList.Contains(collider.gameObject))
            {
                targetList.Add(collider.gameObject);
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
