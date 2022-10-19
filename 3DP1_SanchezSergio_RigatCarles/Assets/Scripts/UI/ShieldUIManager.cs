using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text shield_current;
    [SerializeField] TMP_Text shield_max;
    [SerializeField] Slider shield_slider;
    int maxShield;

    public void updateUI(int currentShield)
    {
        shield_current.text = currentShield.ToString();
        shield_max.text = maxShield.ToString();
        shield_slider.value = currentShield/maxShield;
    }

    public void setMaxShield(int maxShield)
    {
        this.maxShield = maxShield;
    }
}
