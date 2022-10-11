using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float initialHealth;
    float currentHealth;

    [SerializeField] UnityEvent onDeath;

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
    {Debug.Log("prevo");
        onDeath.Invoke();
        Debug.Log("EIIII");
        Destroy(gameObject);
    }
}
