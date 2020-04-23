using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [SerializeField] private GameObject[] missionPrefabs;
    [SerializeField] private int currentMission;
    private bool canTalkToMissionGiver = true;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.EndMission += EndMission;
        EventManager.PlayerAtMissionGiver += PlayerAtMissionGiver;
        EventManager.PlayerLeftMissionGiver += PlayerLeftMissionGiver;
        EventManager.InstantiateNextMission += InstantiateNextMission;
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
        StopAllCoroutines();
    }

    private void PlayerSpokeToMissionGiver()
    {
        if (canTalkToMissionGiver)
        {
            canTalkToMissionGiver = false;
            EventManager.TriggerPlayerSpokeToMissionGiver(missionPrefabs[currentMission].GetComponent<MissionFetch>().npcDialog);
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

    // Update is called once per frame
    void Update()
    {
        
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
