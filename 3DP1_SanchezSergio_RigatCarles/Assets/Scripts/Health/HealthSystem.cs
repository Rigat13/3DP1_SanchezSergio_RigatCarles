using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float initialHealth;
    [SerializeField] public float maxHealth;
    public float currentHealth;

    [SerializeField] UnityEvent onDeath;

    void Awake() 
    {
        currentHealth = initialHealth;
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        EnemyAI enem = gameObject.GetComponent<EnemyAI>();
        Debug.Log(currentHealth);
        enem.getHit();
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


    private void die()
    {
        onDeath.Invoke();
    }
}
