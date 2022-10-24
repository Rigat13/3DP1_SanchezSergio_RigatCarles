using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Instantiate(objectToActivate);
    }
}
