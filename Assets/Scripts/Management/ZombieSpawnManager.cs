using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawnManager : MonoBehaviour
{
    public static ZombieSpawnManager instance;

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
    private List<GameObject> missionZombies = new List<GameObject>();

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject player;

    private GameObject[] missionSpawnPoints;
    private int missionZombiesToSpawn;
    private bool shouldSpawnMissionZombies = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = PlayerManager.instance.player;
        EventManager.zombieShouldDespawn += DespawnZombie;
    }

    private void FixedUpdate()
    {
        HandleSpawning();
        if (shouldSpawnMissionZombies)
        {
            HandleMissionSpawning();
        }
    }

    private void DespawnZombie(GameObject zombie)
    {
        if(!zombies.Remove(zombie)) missionZombies.Remove(zombie);
        Destroy(zombie.gameObject);
    }

    private void HandleSpawning()
    {
        //Debug.Log("Zombies: " + zombies.Count.ToString());
        if (zombies.Count >= maxZombies) return;

        List<GameObject> validSpawns = new List<GameObject>();

        foreach (GameObject spawnPoint in spawnPoints)
        {
            float dist = Vector3.Distance(player.transform.position, spawnPoint.transform.position);

            if (dist > minSpawnDistance && dist < maxSpawnDistance) validSpawns.Add(spawnPoint);
        }

        if (validSpawns.Count == 0)
        {
            //Debug.Log("No valid spawns found!");
            return;
        }

        int index = 0;
        float xOffset = 0;
        float zOffset = 0;


        index = Random.Range(0, validSpawns.Count);
        xOffset = Random.Range(-spawnRadius, spawnRadius);
        zOffset = Random.Range(-spawnRadius, spawnRadius);

        Vector3 spawnLoc = validSpawns[index].transform.position;
        spawnLoc.x += xOffset;
        //spawnLoc.y += 0.5f;
        spawnLoc.z += zOffset;

        //NavMeshHit hitInfo;
        //if (!NavMesh.SamplePosition(spawnLoc, out hitInfo, spawnRadius, NavMesh.AllAreas)) continue;

        //spawnLoc = hitInfo.position;

        if (Physics.CheckBox(spawnLoc, new Vector3(0.25f, 0.5f, 0.25f)))
        {
            //Debug.Log("Physics check failed for spawn!");
            return;
        }

        zombies.Add(Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], spawnLoc, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0)));
        //Debug.Log("Zombie spawned successfully!");
    }

    public void SetMissionZombieSpawns(GameObject[] missionSpawnPoints, int maxZombies, bool shouldSpawnZombies)
    {
        this.missionSpawnPoints = missionSpawnPoints;
        missionZombiesToSpawn = maxZombies;
        shouldSpawnMissionZombies = shouldSpawnZombies;
    }

    private void HandleMissionSpawning()
    {
        Debug.LogError("Mission Zombies: " + missionZombies.Count.ToString());
        if (missionZombies.Count >= missionZombiesToSpawn)
        {
            shouldSpawnMissionZombies = false;
            return;
        }

        int index = 0;
        float xOffset = 0;
        float zOffset = 0;


        index = Random.Range(0, missionSpawnPoints.Length);
        xOffset = Random.Range(-spawnRadius, spawnRadius);
        zOffset = Random.Range(-spawnRadius, spawnRadius);

        Vector3 spawnLoc = missionSpawnPoints[index].transform.position;
        spawnLoc.x += xOffset;
        //spawnLoc.y += 0.5f;
        spawnLoc.z += zOffset;

        //NavMeshHit hitInfo;
        //if (!NavMesh.SamplePosition(spawnLoc, out hitInfo, spawnRadius, NavMesh.AllAreas)) continue;

        //spawnLoc = hitInfo.position;

        //if (Physics.CheckBox(spawnLoc, new Vector3(0.25f, 0.5f, 0.25f)))
        //{
           // Debug.LogError("Physics check failed for spawn!");
           // return;
        //}

        missionZombies.Add(Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], spawnLoc, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0)));
        Debug.LogError("Zombie spawned successfully!");
    }
}
