using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float currentHealth;
    public float currentShield;
    [SerializeField] float maxShield;

    [SerializeField] GameObject hud;
    

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void takeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield = currentShield - (0.75f * damage);
            Debug.Log(currentShield);
            currentHealth = currentHealth - (0.25f * damage);
        }
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        if (currentHealth <= 0.0f)
        {
            die();
        }

    }

    public void addHealth(float healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void addShield(float shieldToAdd)
    {
        currentShield += shieldToAdd;
        if (currentShield > maxShield)
            currentShield = maxShield;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "deadzone")
        {
            takeDamage(99999999999999);
            Debug.Log("Get deadzoned");
        }
    }

    private void die()
    {
        hud.SetActive(true);
    }
}
