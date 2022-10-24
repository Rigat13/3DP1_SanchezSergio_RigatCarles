using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] ShootingGallery shootingGallery;
    [SerializeField] Animator animator;

    public void defeated()
    {
        shootingGallery.addPoint();
        animator.SetTrigger("defeated");
    }
}
