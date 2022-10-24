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

    [SerializeField] GameObject firstTimeReward;
    [SerializeField] GameObject blockades;
    bool firstTimeWon = true;

    [SerializeField] Animator winAnimator;

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

    public void hasWon()
    {
        if (firstTimeWon)
        {
            firstTimeWon = false;
            firstTimeReward.SetActive(true);
            blockades.SetActive(false);
        }
        winAnimator.SetTrigger("win");
        deactivate();
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
            hasWon();
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
