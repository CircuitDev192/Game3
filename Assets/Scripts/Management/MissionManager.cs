﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [SerializeField] private GameObject[] missionPrefabs;
    [SerializeField] private int currentMission;
    [SerializeField] private GameObject[] policeStationLights;
    private bool canTalkToMissionGiver = true;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.EndMission += EndMission;
        EventManager.PlayerAtMissionGiver += PlayerAtMissionGiver;
        EventManager.PlayerLeftMissionGiver += PlayerLeftMissionGiver;
        EventManager.InstantiateNextMission += InstantiateNextMission;
        EventManager.PlayerEnteredMissionVehicle += PlayerEnteredMissionVehicle;
    }

    private void PlayerEnteredMissionVehicle()
    {
        PlayerManager.instance.player.SetActive(false);
    }

    private void InstantiateNextMission()
    {
        Instantiate(missionPrefabs[currentMission], Vector3.zero, Quaternion.identity);
    }

    private void PlayerAtMissionGiver()
    {
        StartCoroutine(WaitForPlayerToTalk());
    }

    private void PlayerLeftMissionGiver()
    {
        StopCoroutine(WaitForPlayerToTalk());
    }

    private void PlayerSpokeToMissionGiver()
    {
        if (canTalkToMissionGiver)
        {
            canTalkToMissionGiver = false;
            if (missionPrefabs[currentMission].TryGetComponent<MissionFetch>(out var fetch))
            {
                EventManager.TriggerPlayerSpokeToMissionGiver(fetch.npcDialog);
            } else if (missionPrefabs[currentMission].TryGetComponent<MissionKill>(out var kill))
            {
                EventManager.TriggerPlayerSpokeToMissionGiver(kill.npcDialog);
            }
            else if (missionPrefabs[currentMission].TryGetComponent<MissionSurvive>(out var survive))
            {
                EventManager.TriggerPlayerSpokeToMissionGiver(survive.npcDialog);
            }
            StartCoroutine(StartNextMission());
        }
    }

    private void EndMission()
    {
        if (currentMission != missionPrefabs.Length - 1)
        {
            currentMission++;
            canTalkToMissionGiver = true;
        }
        else
        {
            //Do this after the last mission is ended.
            EventManager.TriggerGameEnded();
        }
    }

    private void DisablePoliceStationLights()
    {
        foreach (GameObject light in policeStationLights)
        {
            light.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartNextMission()
    {
        yield return new WaitForSeconds(10f);
        if (missionPrefabs[currentMission].TryGetComponent<MissionSurvive>(out var survive))
        {
            DisablePoliceStationLights();
            EventManager.TriggerPlayerSpokeToMissionGiver(survive.npcDialog2);
        }
        yield return new WaitForSeconds(10f);
        EventManager.TriggerInstantiateNextMission();
    }

    IEnumerator WaitForPlayerToTalk()
    {
        while (!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        PlayerSpokeToMissionGiver();
    }
}
