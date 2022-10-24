using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create Health Consumer")]
public class HealthAsset : ConsumableAsset
{
    public int healthToAdd;

    override public void consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out HealthSystem health))
        {
            health.addHealth(healthToAdd);
        }
    }
}