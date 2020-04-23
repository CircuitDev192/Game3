using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weatherTiles;
    [SerializeField] private float activeDistance;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject tile in weatherTiles)
        {
            float distance = Vector3.Distance(playerTransform.position, tile.transform.position);

            if (distance > activeDistance) tile.SetActive(false);

            else tile.SetActive(true);
        }
    }
}
