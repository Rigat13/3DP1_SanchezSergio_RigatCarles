using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform weaponShootingDummy;
    [SerializeField] float bulletSpeed;

    void Update() 
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }    
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, weaponShootingDummy.position, weaponShootingDummy.rotation);
        bulletInstance.GetComponent<Rigidbody>().velocity = weaponShootingDummy.forward * bulletSpeed;
    }
}
