using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<Consumable>(out Consumable consumable))
        {
            consumable.consume(gameObject);
        }
    }
}
