using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private List<PlayerTypeData> playerTypes = new List<PlayerTypeData>();

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public float raycastHeight;
    public string groundLayerName;

    public float spawnCooldown;
    private float spawnTimer;

    private int groundLayerIndex;
    private bool isSpawning = false;

    private void Awake()
    {
        groundLayerIndex = LayerMask.NameToLayer(groundLayerName);
    }
    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (isSpawning && spawnTimer < 0 )
            SpawnNPC();
    }

    public void StartSpawningLoop()
    {
        isSpawning = true;
    }

    private void SpawnNPC()
    {
        if(GetNewSpawnPoint(out Vector3 point))
        {
            GameObject newNPC = Instantiate(npcPrefab, point, Quaternion.identity);

            PlayerTypeData randomPlayerType = playerTypes[Random.Range(0, playerTypes.Count)];

            newNPC.GetComponent<PlayerTypeHandler>().playerTypeData = randomPlayerType;

            newNPC.gameObject.name = randomPlayerType.name;

            newNPC.GetComponent<NameHandler>().TMPName.text = randomPlayerType.name;

            GameManager.instance.AddPlayer(newNPC.GetComponent<NPCController>());

            spawnTimer = spawnCooldown * GameManager.instance.playerList.Count / 5;
        }
    }

    private bool GetNewSpawnPoint(out Vector3 point)
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 rayOrigin = new Vector3(randomX, raycastHeight, randomZ);


        if (Physics.Raycast(rayOrigin, Vector3.down , out RaycastHit hit, raycastHeight))
        {
            if(hit.collider.gameObject.layer == groundLayerIndex)
            {
                Debug.DrawRay(rayOrigin, Vector3.down * raycastHeight, Color.white, 3f);

                point = hit.point;
                if(CheckAreaForCharacters(point))
                return true;
            }
        }
        point = Vector3.zero;
        return false;
    }

    private bool CheckAreaForCharacters(Vector3 point)
    {
        Collider[] colliders = Physics.OverlapSphere(point, 100f);

        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Human") || c.CompareTag("Zombie"))
                return false;
        }
        return true;
    }
}
