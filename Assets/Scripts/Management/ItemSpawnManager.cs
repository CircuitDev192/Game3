using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    private List<GameObject> items = new List<GameObject>();
    private List<GameObject> spawnables = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.EndMission += EndMission;
        foreach (Transform childItem in transform)
        {
            items.Add(childItem.gameObject);
            spawnables.Add(childItem.gameObject);
            spawnPoints.Add(childItem);
            //get posistion of the items and them to list of transforms or vector3s
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndMission()
    {
        if (items.Count != 0)
        {
            items.Clear();
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject newItem = Instantiate(spawnables[spawnPoints.IndexOf(spawnPoint)], spawnPoint.position, Quaternion.identity, this.transform);
            items.Add(newItem);
        }
    }

    private void OnDestroy()
    {
        EventManager.EndMission -= EndMission;
    }
}
