using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create Key Consumer")]
public class KeyAsset : ConsumableAsset
{
    public Door doorToUnlock;

    override public bool consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out PlayerStats playerStats))
        {
            return doorToUnlock.unlock();
        }
        return false;
    }
}