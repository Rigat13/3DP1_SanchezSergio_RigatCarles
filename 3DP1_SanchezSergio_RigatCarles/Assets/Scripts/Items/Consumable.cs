using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] ConsumableAsset consumableAsset;
    public void consume(GameObject consumer)
    {
        Debug.Log("En consumer: "+consumer.name+" m'ha consumit: ");
        consumableAsset.consume(consumer);
        Destroy(gameObject);
    }
}
