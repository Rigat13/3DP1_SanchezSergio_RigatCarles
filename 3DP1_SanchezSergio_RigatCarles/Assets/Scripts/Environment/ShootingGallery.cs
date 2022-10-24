using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] Timer timer;
    public void activate()
    {
        timer.startTimer();
    }

    public void deactivate()
    {
        timer.stopTimer();
    }
}
