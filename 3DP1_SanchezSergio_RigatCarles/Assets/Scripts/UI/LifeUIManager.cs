using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LifeUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text life_current;
    [SerializeField] TMP_Text life_max;
    [SerializeField] Slider life_slider;
    int maxLife;

    public void updateUI(int currentLife)
    {
        life_current.text = currentLife.ToString();
        life_max.text = maxLife.ToString();
        life_slider.value = currentLife/maxLife;
    }

    public void setMaxLife(int maxLife)
    {
        this.maxLife = maxLife;
    }
}
