using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionDialogController : MonoBehaviour
{
    [SerializeField] private Text talkPrompt;
    [SerializeField] private Text missionDialog;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.PlayerAtMissionGiver += PlayerAtMissionGiver;
        EventManager.PlayerLeftMissionGiver += PlayerLeftMissionGiver;
        EventManager.PlayerSpokeToMissionGiver += PlayerSpokeToMissionGiver;
    }

    private void PlayerSpokeToMissionGiver(string npcDialog)
    {
        talkPrompt.gameObject.SetActive(false);
        missionDialog.gameObject.SetActive(true);
        missionDialog.text = npcDialog;
        StartCoroutine(StartNextMission());
    }

    private void PlayerLeftMissionGiver()
    {
        talkPrompt.gameObject.SetActive(false);
    }

    private void PlayerAtMissionGiver()
    {
        talkPrompt.gameObject.SetActive(true);
    }

    IEnumerator StartNextMission()
    {
        yield return new WaitForSeconds(20f);
        EventManager.TriggerInstantiateNextMission();
        missionDialog.gameObject.SetActive(false);
    }
}
