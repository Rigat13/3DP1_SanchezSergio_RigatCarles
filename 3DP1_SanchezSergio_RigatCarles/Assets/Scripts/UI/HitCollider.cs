using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitCollider : MonoBehaviour
{
    [SerializeField] HealthSystem hs;
    [SerializeField] float damageMultiplier;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float speedMultiplier; // X, no sobreescriure canvis de velocitat de l'EnemyAI segons estat

    public void takeDamage(float damage)
    {
        Debug.Log("Punto debil");
        hs.takeDamage(damage * damageMultiplier);
        agent.speed *= speedMultiplier;
    }
}
