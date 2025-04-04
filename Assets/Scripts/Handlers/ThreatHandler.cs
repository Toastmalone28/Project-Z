using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatHandler : MonoBehaviour
{
    public float detectionRadius;
    public LayerMask hittablesLayer;
    public LayerMask obstacleLayer;

    public float scanFrequency;
    private float scanInterval;
    private float scanTimer;

    public float memoryDuration;

    private SerializedDictionary<GameObject, Vector3> enemyMemory = new SerializedDictionary<GameObject, Vector3>();
    private List<GameObject> visibleEnemies = new List<GameObject>();
    private List<GameObject> detectedEnemies = new List<GameObject>();

    public List<GameObject> VisibleEnemies
    {
        get
        {
            visibleEnemies.RemoveAll(obj => !obj);
            return visibleEnemies;
        }
    }
    public List<GameObject> DetectedEnemies
    {
        get
        {
            detectedEnemies.RemoveAll(obj => !obj);
            return detectedEnemies;
        }
    }

    private void Awake()
    {
        scanInterval = 1f / scanFrequency;
    }

    private void FixedUpdate()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            ScanEnvironment();
        }
    }

    private void ScanEnvironment()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, detectionRadius, hittablesLayer);
        GetTargetsInView(enemyColliders);
    }

    private void GetTargetsInView(Collider[] enemyColliders)
    {
        List<GameObject> currentlyVisible = new List<GameObject>();
        HashSet<GameObject> currentlyDetected = new HashSet<GameObject>();

        foreach (Collider collider in enemyColliders)
        {
            GameObject enemy = collider.gameObject;
            if (enemy.transform.IsChildOf(transform)) continue;

            currentlyDetected.Add(enemy);

            if (IsInView(enemy))
            {
                currentlyVisible.Add(enemy);
                if (!visibleEnemies.Contains(enemy))
                {
                    visibleEnemies.Add(enemy);
                }

                enemyMemory[enemy] = enemy.transform.position;
            }
        }

        foreach (GameObject enemy in visibleEnemies)
        {
            if (!currentlyVisible.Contains(enemy))
            {
                StartCoroutine(RememberEnemyPosition(enemy));
            }
        }

        visibleEnemies = currentlyVisible;

        List<GameObject> outOfRangeEnemies = new List<GameObject>();
        foreach (GameObject enemy in detectedEnemies)
        {
            if (!currentlyDetected.Contains(enemy))
            {
                outOfRangeEnemies.Add(enemy);
            }
        }

        foreach (GameObject enemy in outOfRangeEnemies)
        {
            StartCoroutine(RememberEnemyPosition(enemy));
        }

        detectedEnemies = new List<GameObject>(currentlyDetected);
    }

    private bool IsInView(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        return !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer);
    }

    private IEnumerator RememberEnemyPosition(GameObject enemy)
    {
        if (!enemyMemory.ContainsKey(enemy))
        {
            enemyMemory[enemy] = enemy.transform.position;
        }

        yield return new WaitForSeconds(memoryDuration);

        if (!visibleEnemies.Contains(enemy) && !detectedEnemies.Contains(enemy))
        {
            enemyMemory.Remove(enemy);
        }
    }

    public Vector3? GetLastKnownPosition(GameObject enemy)
    {
        if (enemyMemory.ContainsKey(enemy))
        {
            return enemyMemory[enemy];
        }
        return null;
    }
    public GameObject GetClosestInVision()
    {
        float distance = Mathf.Infinity;
        GameObject closest = null;
        foreach (GameObject go in visibleEnemies)
        {
            float d = Vector3.Distance(transform.position, go.transform.position);
            if (d < distance)
            {
                closest = go;
                distance = d;
            }
        }
        return closest;
    }

    public GameObject GetClosestInVision(string tag)
    {
        float distance = Mathf.Infinity;
        GameObject closest = null;
        foreach (GameObject go in visibleEnemies)
        {
            if(!go.CompareTag(tag))
                continue;

            float d = Vector3.Distance(transform.position, go.transform.position);
            if (d < distance)
            {
                closest = go;
                distance = d;
            }
        }
        return closest;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
