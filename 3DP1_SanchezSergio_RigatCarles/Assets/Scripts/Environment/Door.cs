using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool locked = true;

    [SerializeField] GameObject worldToSpawnDespawn;
    [SerializeField] bool spawnOnUnlock = false;

    public bool unlock()
    {
        if (!locked) return false;
        locked = false;
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !locked)
        {
            animator.SetTrigger("open");
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !locked)
        {
            animator.SetTrigger("close");
        }
    }
}
