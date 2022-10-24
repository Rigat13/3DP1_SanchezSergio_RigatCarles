using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Animator targetsAnimator;
    [SerializeField] List<Target> targets;
    int points;
    [SerializeField] UnityEvent<int> onStart;
    [SerializeField] UnityEvent<int> onScore;

    public void activate()
    {
        onStart.Invoke(targets.Count);
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
        onScore.Invoke(points);
        if (points == targets.Count)
        {
            deactivate();
        }
    }
}
