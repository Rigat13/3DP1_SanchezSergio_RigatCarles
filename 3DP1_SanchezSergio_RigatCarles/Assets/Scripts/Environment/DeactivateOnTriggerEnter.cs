using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject objectToDeactivate;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Destroy(objectToDeactivate);
    }
}
