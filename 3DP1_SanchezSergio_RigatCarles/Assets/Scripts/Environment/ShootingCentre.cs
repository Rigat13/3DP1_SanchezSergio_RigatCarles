using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCentre : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ShootingGallery shootingGallery;
   [SerializeField] bool canActivate = true;
   [SerializeField] bool canDeactivate = false;
    float timeToFullyActivate = 1f;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player" && canActivate) {
            canActivate = false;
            animator.SetTrigger("activate");
            shootingGallery.activate();
            StartCoroutine(WaitToFullyActivate());
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player" && canDeactivate) {
            canDeactivate = false;
            animator.SetTrigger("deactivate");
            shootingGallery.deactivate();
            StartCoroutine(WaitToFullyActivate());
        }
    }

    public void restart()
    {
        canActivate = true;
        canDeactivate = false;
    }

    IEnumerator WaitToFullyActivate() {
        yield return new WaitForSeconds(timeToFullyActivate);
        canDeactivate = true;
    }

    IEnumerator WaitToFullyDeactivate() {
        yield return new WaitForSeconds(timeToFullyActivate);
        canActivate = true;
    }
}
