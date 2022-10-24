using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitCollider : MonoBehaviour
{
    [SerializeField] HealthSystem hs;
    [SerializeField] float damageMultiplier;
    [SerializeField] ParticleSystem weakPointPart;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound_hit;

    [SerializeField] NavMeshAgent agent;

    public void takeDamage(float damage)
    {
        audioSource.PlayOneShot(sound_hit);
        weakPointPart.Play();
        hs.takeDamage(damage * damageMultiplier);

    }
}
