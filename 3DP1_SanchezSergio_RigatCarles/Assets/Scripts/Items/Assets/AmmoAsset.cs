using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create Ammo Consumer")]
public class AmmoAsset : ConsumableAsset
{
    public int ammoToAdd;

    override public bool consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out PlayerStats playerStats))
        {
            return playerStats.addAmmo(ammoToAdd);
        }
        return false;
    }
}

