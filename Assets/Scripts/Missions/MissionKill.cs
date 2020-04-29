﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionKill : MonoBehaviour
{
    [SerializeField] private string missionTitle;
    [SerializeField] private string missionDescription;
    [SerializeField] private string returnToBaseDescription; //Shows after the player completes the objective
    [SerializeField] public string npcDialog; //What the NPC tells the player at the start of the mission
    [SerializeField] private GameObject missionStartCollider;
    [SerializeField] private GameObject missionObjectiveAreaCollider;
    [SerializeField] private GameObject missionAreaToClear;
    [SerializeField] private GameObject missionEndCollider;
    [SerializeField] private Transform missionObjectiveLocation;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private int zombiesToSpawn;
    [SerializeField] private bool shouldSpawnZombiesAtMissionArea;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.PlayerClearedArea += PlayerClearedArea;
        EventManager.StartMission += StartMission;
        EventManager.EndMission += EndMission;
        EventManager.PlayerAtMissionArea += PlayerAtMissionArea;
    }

    private void PlayerAtMissionArea()
    {
        ZombieSpawnManager.instance.SetMissionZombieSpawns(spawnPoints, zombiesToSpawn, shouldSpawnZombiesAtMissionArea, false);
        missionAreaToClear.gameObject.GetComponent<ZombieAreaToClear>().shouldCountDown = true;
    }

    private void StartMission()
    {
        missionStartCollider.gameObject.SetActive(false);
        EventManager.TriggerMissionChanged(missionTitle, missionDescription);
        EventManager.TriggerMissionWaypointChanged(missionObjectiveLocation.position);
    }

    private void EndMission()
    {
        missionEndCollider.gameObject.SetActive(false);
        EventManager.TriggerMissionChanged("Free Roam", "Gear up, explore, or accept the next mission.");
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    private void PlayerClearedArea()
    {
        missionEndCollider.gameObject.SetActive(true);
        EventManager.TriggerMissionChanged(missionTitle, returnToBaseDescription);
        missionObjectiveLocation = missionEndCollider.transform;
        EventManager.TriggerMissionWaypointChanged(missionObjectiveLocation.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        EventManager.PlayerClearedArea -= PlayerClearedArea;
        EventManager.StartMission -= StartMission;
        EventManager.EndMission -= EndMission;
        EventManager.PlayerAtMissionArea -= PlayerAtMissionArea;
    }
}
