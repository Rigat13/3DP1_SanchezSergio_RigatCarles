using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTimeParticles : DestroyAfterTime
{
    public override void OnEnable() 
    {
        gameObject.transform.parent = poolParent;
        gameObject.SetActive(false);
    }
}
