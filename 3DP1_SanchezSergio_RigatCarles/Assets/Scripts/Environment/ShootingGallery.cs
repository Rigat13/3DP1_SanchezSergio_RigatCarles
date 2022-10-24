using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Animator targetsAnimator;
    public void activate()
    {
        timer.startTimer();
        targetsAnimator.SetTrigger("activate");
    }

    public void deactivate()
    {
        timer.stopTimer();
        targetsAnimator.SetTrigger("deactivate");
    }
}
