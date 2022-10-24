using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create Shield Consumer")]
public class ShieldAsset : ConsumableAsset
{
    public int shieldToAdd;

    override public bool consume(GameObject consumer)
    {
        if (consumer.TryGetComponent(out PlayerStats playerStats))
        {
            return playerStats.addShield(shieldToAdd);
        }
        return false;
    }
}
