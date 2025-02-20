using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public List<GameObject> prefabs = new List<GameObject>();
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float spawnRadius;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnPrefab();
        }
    }
    private void SpawnPrefab()
    {
        if (prefabs.Count == 0)
        {
            Debug.LogWarning(gameObject.name + " has no prefabs assigned to it!");
            return;
        }

        //Get random prefab from list
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];

        //Get random position in spawn radius
        Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(randomPos.x, 0, randomPos.y) + transform.position;

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, gameObject.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
