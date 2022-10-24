using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Animator targetsAnimator;
    [SerializeField] Animator centreAnimator;
    [SerializeField] ShootingCentre shootingCentre;

    [SerializeField] List<Target> targets;
    int points;
    [SerializeField] UnityEvent<int> onStart;
    [SerializeField] UnityEvent<int> onScore;

    bool firstTime;

    public void activate()
    {
        if (!firstTime) firstTime = true;
        else restart();
        
        points = 0;

        onStart.Invoke(targets.Count);
        onScore.Invoke(points);
        
        timer.startTimer();
        targetsAnimator.SetTrigger("activate");
    }

    public void deactivate()
    {
        timer.stopTimer();
        targetsAnimator.SetTrigger("deactivate");
        centreAnimator.SetTrigger("deactivate");
    }

    public void addPoint()
    {
        points++;
        onScore.Invoke(points);
        if (points == targets.Count)
            deactivate();
    }

    public void timerEnded()
    {
        restart();
        if (points < targets.Count)
        {
            targetsAnimator.SetTrigger("deactivate");
            centreAnimator.SetTrigger("deactivate");
        }
    }

    void restart()
    {
        foreach (Target target in targets)
            target.restart();
        shootingCentre.restart();
    }
}
