using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffectsController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
