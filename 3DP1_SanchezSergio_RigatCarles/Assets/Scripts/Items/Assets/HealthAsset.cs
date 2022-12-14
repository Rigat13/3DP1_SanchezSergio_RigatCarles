using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create Health Consumer")]
public class HealthAsset : ConsumableAsset
{
    public int healthToAdd;

    override public bool consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out PlayerStats playerStats))
        {
            return playerStats.addHealth(healthToAdd);
        }
        return false;
    }
}