using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool locked = true;

    [SerializeField] int keyCodeToUnlock = 0;

    public bool unlock(int keyCodeToUnlock)
    {
        if (keyCodeToUnlock != this.keyCodeToUnlock) return false;
        if (!locked) return false;
        locked = false;
        open();
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !locked)
            open();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !locked)
            close();
    }

    void open()
    {
        animator.SetTrigger("open");
    }

    void close()
    {
        animator.SetTrigger("close");
    }
}
