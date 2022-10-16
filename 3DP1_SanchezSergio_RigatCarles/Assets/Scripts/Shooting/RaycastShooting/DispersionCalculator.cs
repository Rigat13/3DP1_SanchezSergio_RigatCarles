using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispersionCalculator : MonoBehaviour
{
    [SerializeField] Camera camera;

    public Ray calculateDispersion(float dispersion, float maxDistance)
    {
        //return camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 randomPoint = Random.insideUnitCircle * dispersion;
        Vector3 point = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane)) + camera.transform.forward * maxDistance + randomPoint;
        Ray ray = new Ray(camera.transform.position, point - camera.transform.position);
        return ray;
    }
}
