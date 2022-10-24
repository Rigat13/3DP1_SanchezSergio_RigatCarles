using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] ShootingGallery shootingGallery;
    [SerializeField] Animator animator;
    bool isDefeated;

    public void restart()
    {
        isDefeated = false;
        animator.SetTrigger("restart");
    }

    public void defeated()
    {
        if (!isDefeated)
        {
            isDefeated = true;
            animator.SetTrigger("defeated");
            shootingGallery.addPoint();
        }
    }
}
