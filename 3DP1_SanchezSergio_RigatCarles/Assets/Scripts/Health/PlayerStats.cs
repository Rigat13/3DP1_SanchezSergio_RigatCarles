using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    public float currentHealth;
    public float currentShield;
    [SerializeField] float maxShield;
    [SerializeField] GameObject hud;
    [SerializeField] Door doorToUnclock;
    
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
        if (currentHealth <= 0.0f)
        {
            currentHealth = 0;
            die();
        }
    }

    public bool addHealth(float healthToAdd)
    {
        if (currentHealth >= maxHealth) return false;

        currentHealth += healthToAdd;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        return true;
    }

    public bool addShield(float shieldToAdd)
    {
        if (currentShield >= maxShield) return false;

        currentShield += shieldToAdd;
        if (currentShield > maxShield) currentShield = maxShield;
        return true;
    }

    public bool addAmmo(int ammoToAdd)
    {
        return GameObject.Find("Player").GetComponent<RaycastShooting>().addAmmo(ammoToAdd);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.tag=="deadzone")
        {
            takeDamage(99999999999999);
            takeDamage(10);
            Debug.Log("Get deadzoned");
        }
    }

    private void die()
    {
       SceneManager.LoadScene(2);
    }

    public bool unlock(int keyCodeToUnclock)
    {
        return doorToUnclock.unlock(keyCodeToUnclock);
    }
}
