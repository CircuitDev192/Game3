using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    private Behaviour[] behavioursToDisable;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if (!isLocalPlayer)
        {
            foreach (Behaviour behaviour in behavioursToDisable)
            {
                behaviour.enabled = false;
            }
        } 
        else
        {
            if (cam != null)
            {
                cam.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if (cam != null)
        {
            cam.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
