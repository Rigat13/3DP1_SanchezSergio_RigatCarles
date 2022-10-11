using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableAsset: ScriptableObject
{
    abstract public void consume(GameObject consumer);
}