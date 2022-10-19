using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text ammo_currentInside;
    [SerializeField] TMP_Text ammo_availableStorage;

    public void updateUI (int currentAmmo, int totalAmmo)
    {
        ammo_currentInside.text = currentAmmo.ToString();
        ammo_availableStorage.text = "/ " + totalAmmo;
    }
}