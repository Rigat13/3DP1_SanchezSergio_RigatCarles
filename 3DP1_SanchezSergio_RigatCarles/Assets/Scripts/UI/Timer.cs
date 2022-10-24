using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float time = 100.0f;
    float initialTime;
    [SerializeField] float endingTime = 10.0f;
    bool isEndingTime;
    [SerializeField] Animator animator;
    

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip endingTimeClip;
    [SerializeField] AudioClip endClip;

    void Start()
    {
        initialTime = time;
    }

    public void startTimer()
    {
        animator.SetTrigger("start");
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        float time = initialTime;
        while (time >= 0.0f)
        {
            if (time <= endingTime) startEndingTimer();
            time -= Time.deltaTime;
            timerText.text = time > 0.0f ? time.ToString("F2") : "0.00";
            yield return null;
        }
        endTimer();
    }

    void startEndingTimer()
    {
        if (isEndingTime) return;
        isEndingTime = true;
        animator.SetTrigger("startEnding");
        audioSource.PlayOneShot(endingTimeClip);
    }

    void endTimer()
    {
        animator.SetTrigger("end");
        isEndingTime = false;
        audioSource.PlayOneShot(endClip);
    }

    public void stopTimer()
    {
        StopAllCoroutines();
        endTimer();
    }
}
