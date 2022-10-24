using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    ShootingGallery shootingGallery;
    Animator animator;

    public void defeated()
    {
        shootingGallery.addPoint();
        animator.SetTrigger("defeated");
    }
}
