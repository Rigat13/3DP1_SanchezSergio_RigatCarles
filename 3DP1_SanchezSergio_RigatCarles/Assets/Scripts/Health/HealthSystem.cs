using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float initialHealth;
    float currentHealth;

    void Awake() 
    {
        currentHealth = initialHealth;
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }
}
