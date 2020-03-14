using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int maxZombies = 50;
    [SerializeField]
    private float spawnRadius = 20.0f;
    [SerializeField]
    private float minSpawnDistance = 50.0f;
    [SerializeField]
    private float maxSpawnDistance = 200.0f;

    [SerializeField]
    private GameObject[] zombiePrefabs;
    private List<GameObject> zombies = new List<GameObject>();

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject player;

    private void Start()
    {
        EventManager.zombieShouldDespawn += DespawnZombie;
    }

    private void FixedUpdate()
    {
        HandleSpawning();
    }

    private void DespawnZombie(GameObject zombie)
    {
        if(!zombies.Remove(zombie)) Debug.Log("Failed to remove zombie from list.");

        Destroy(zombie.gameObject);
    }

    private void HandleSpawning()
    {
        Debug.Log("Zombies: " + zombies.Count.ToString());
        if (zombies.Count >= maxZombies) return;

        List<GameObject> validSpawns = new List<GameObject>();

        foreach(GameObject spawnPoint in spawnPoints)
        {
            float dist = Vector3.Distance(player.transform.position, spawnPoint.transform.position);

            if (dist > minSpawnDistance && dist < maxSpawnDistance) validSpawns.Add(spawnPoint);
        }

        if (validSpawns.Count == 0) return;

        int index = 0;
        float xOffset = 0;
        float zOffset = 0;

        while(zombies.Count < maxZombies)
        {
            index = Random.Range(0, validSpawns.Count);
            xOffset = Random.Range(-spawnRadius, spawnRadius);
            zOffset = Random.Range(-spawnRadius, spawnRadius);

            Vector3 spawnLoc = validSpawns[index].transform.position;
            spawnLoc.x += xOffset;
            spawnLoc.y += 0.5f;
            spawnLoc.z += zOffset;

            //NavMeshHit hitInfo;
            //if (!NavMesh.SamplePosition(spawnLoc, out hitInfo, spawnRadius, NavMesh.AllAreas)) continue;

            //spawnLoc = hitInfo.position;

            if (Physics.CheckBox(spawnLoc, new Vector3(0.25f, 0.5f, 0.25f))) continue;

            zombies.Add(Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], spawnLoc, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0)));
        }
    }
}
