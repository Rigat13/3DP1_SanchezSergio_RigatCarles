using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] protected float time;
    [SerializeField] protected Transform poolParent;

    public abstract void OnEnable();
}
