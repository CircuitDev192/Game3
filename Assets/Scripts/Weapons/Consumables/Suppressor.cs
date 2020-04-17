using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suppressor : MonoBehaviour
{
    [SerializeField] public bool isEquipped = false;
    [SerializeField] private float durability = 100f;

    public void UpdateDurability(float fatigueAmount)
    {
        durability -= fatigueAmount;
        if (durability <= Mathf.Epsilon)
        {
            durability = 0f;
            EventManager.TriggerSuppressorBroken();
        }
        EventManager.TriggerSuppressorDurabilityChanged(durability);
    }

    public float GetDurability()
    {
        return durability;
    }
}
