using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : WeaponContext
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeContext();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState(this);
    }    
}