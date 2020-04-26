using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSurvive : MonoBehaviour
{
    [SerializeField] private string missionTitle;
    [SerializeField] private string missionDescription;
    [SerializeField] private string returnToBaseDescription; //Shows after the player completes the objective
    [SerializeField] public string npcDialog; //What the NPC tells the player at the start of the mission
    [SerializeField] public string npcDialog2; //Second half of dialog. Specific to the last mission..
    [SerializeField] private Transform missionObjectiveLocation;
    [SerializeField] private Transform missionObjective2Location;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private int zombiesToSpawn;
    [SerializeField] private bool shouldSpawnZombiesAtMissionArea;
    [SerializeField] private GameObject flarePrefab;
    [SerializeField] private Transform[] flareSpawnLocations; // must have 4 total

    // Start is called before the first frame update
    void Start()
    {
        EventManager.EndMission += EndMission;
        EventManager.PlayerEnteredMissionVehicle += PlayerEnteredMissionVehicle;
        EventManager.PlayerClearedArea += PlayerClearedArea;

        EventManager.TriggerMissionChanged(missionTitle, missionDescription);
        EventManager.TriggerMissionWaypointChanged(missionObjectiveLocation.position);
        EventManager.TriggerStartMission();
        EventManager.TriggerFinalMissionInstantiated();
    }

    private void PlayerEnteredMissionVehicle()
    {
        ZombieSpawnManager.instance.SetMissionZombieSpawns(spawnPoints, zombiesToSpawn, shouldSpawnZombiesAtMissionArea);
        EventManager.TriggerMissionWaypointChanged(missionObjective2Location.position);

        StartCoroutine(FireFlares());
    }

    private void EndMission()
    {
        EventManager.TriggerMissionChanged("", "");
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    private void PlayerClearedArea()
    {
        EventManager.TriggerMissionChanged(missionTitle, returnToBaseDescription);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FireFlares()
    {
        yield return new WaitForSeconds(5f);
        GameObject flare = Instantiate(flarePrefab, flareSpawnLocations[0].position, Quaternion.identity);
        flare.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 3000f);

        yield return new WaitForSeconds(1f);
        flare = Instantiate(flarePrefab, flareSpawnLocations[1].position, Quaternion.identity);
        flare.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 3000f);

        yield return new WaitForSeconds(0.3f);
        flare = Instantiate(flarePrefab, flareSpawnLocations[2].position, Quaternion.identity);
        flare.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 3000f);

        yield return new WaitForSeconds(1f);
        flare = Instantiate(flarePrefab, flareSpawnLocations[3].position, Quaternion.identity);
        flare.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 3000f);
    }
}
