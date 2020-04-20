using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MonoBehaviour
{
    [SerializeField] private string missionTitle;
    [SerializeField] private string missionDescription;
    [SerializeField] private string returnToBaseDescription; //Shows after the player completes the objective
    [SerializeField] private GameObject missionStartCollider;
    [SerializeField] private GameObject missionObjectiveAreaCollider;
    [SerializeField] private GameObject missionEndCollider;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private int zombiesToSpawn;
    [SerializeField] private bool shouldSpawnZombiesAtMissionArea;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.PlayerPickedUpMissionItem += PlayerPickedUpMissionItem;
        EventManager.StartMission += StartMission;
        EventManager.EndMission += EndMission;
        EventManager.PlayerAtMissionArea += PlayerAtMissionArea;
    }

    private void PlayerAtMissionArea()
    {
        ZombieSpawnManager.instance.SetMissionZombieSpawns(spawnPoints, zombiesToSpawn, shouldSpawnZombiesAtMissionArea);
    }

    private void StartMission()
    {
        missionStartCollider.gameObject.SetActive(false);
        EventManager.TriggerMissionChanged(missionTitle, missionDescription);
    }

    private void EndMission()
    {
        missionEndCollider.gameObject.SetActive(false);
        EventManager.TriggerMissionChanged("", "");
        Destroy(this.gameObject);
    }

    private void PlayerPickedUpMissionItem()
    {
        missionEndCollider.gameObject.SetActive(true);
        EventManager.TriggerMissionChanged(missionTitle, returnToBaseDescription);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
