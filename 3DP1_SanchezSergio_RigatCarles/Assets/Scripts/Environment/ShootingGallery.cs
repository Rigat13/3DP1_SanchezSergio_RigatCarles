using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Animator targetsAnimator;
    [SerializeField] List<Target> targets;
    int points;

    public void activate()
    {
        points = 0;
        timer.startTimer();
        targetsAnimator.SetTrigger("activate");
    }

    public void deactivate()
    {
        timer.stopTimer();
        targetsAnimator.SetTrigger("deactivate");
    }

    public void addPoint()
    {
        points++;
        if (points == targets.Count)
        {
            deactivate();
        }
    }
}
