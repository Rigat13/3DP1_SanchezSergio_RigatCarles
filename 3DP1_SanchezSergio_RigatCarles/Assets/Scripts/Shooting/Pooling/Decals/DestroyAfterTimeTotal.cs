using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeTotal : DestroyAfterTime
{
    public override void OnEnable() 
    {
        StartCoroutine(destroyAfterTime(time));
    }

    IEnumerator destroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
