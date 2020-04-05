using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfoController : MonoBehaviour
{
    [SerializeField] private Text weaponName;
    [SerializeField] private Text roundsInMag;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.WeaponChanged += WeaponNameChanged;
        EventManager.AmmoCountChanged += RoundsInMagChanged;
    }

    private void WeaponNameChanged(string weaponName)
    {
        this.weaponName.text = weaponName;
    }

    private void RoundsInMagChanged(int roundsInMag)
    {
        this.roundsInMag.text = roundsInMag.ToString("D2");
    }
}
