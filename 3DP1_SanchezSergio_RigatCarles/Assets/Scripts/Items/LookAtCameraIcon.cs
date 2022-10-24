using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraIcon : MonoBehaviour
{
    Vector3 cameraPosition;
    void Update()
    {
        cameraPosition = Camera.main.transform.position;
        transform.LookAt(cameraPosition);
    }
}
