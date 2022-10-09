using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text ammo_currentInside;
    [SerializeField] TMP_Text ammo_availableStorage;

    public void uiAmmoUpdate (int currentAmmo, int totalAmmo)
    {
        ammo_currentInside.text = currentAmmo.ToString();
        ammo_availableStorage.text = "/ " + totalAmmo;
    }
}
