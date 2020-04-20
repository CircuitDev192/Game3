using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] missionPrefabs;
    [SerializeField] private int currentMission;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(missionPrefabs[currentMission], Vector3.zero, Quaternion.identity);
        EventManager.EndMission += EndMission;
    }

    private void EndMission()
    {
        if (currentMission != missionPrefabs.Length - 1)
        {
            currentMission++;
            Instantiate(missionPrefabs[currentMission], Vector3.zero, Quaternion.identity);
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
}
