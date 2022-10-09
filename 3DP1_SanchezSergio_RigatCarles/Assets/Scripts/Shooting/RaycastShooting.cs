using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastShooting : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] ObjectPool decalPool;
    
    [Header("Shooting")]
    [SerializeField] LayerMask shootingMask;
    [SerializeField] WeaponType weapon;
    [SerializeField] Transform weaponDummy;
    [SerializeField] Transform weaponShootingDummy;
    Ray ray;

    [Header("Reloading")]
    [SerializeField] KeyCode reloadKey = KeyCode.R;

    [Header("VFX")]
    [SerializeField] GameObject decalParticles;
    [SerializeField] GameObject decalImage;
    [SerializeField] float zOffset;
    [SerializeField] GameObject weaponParticles;

    [Header("UI")]
    [SerializeField] UnityEvent<int, int> ammoUpdate;

    void Start()
    {
        Instantiate(weapon.getWeaponModel(), weaponDummy);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            bool hasShooted = weapon.shoot();
            if(hasShooted) { raycastShoot(); updateAmmoUI();}
        }
        if (Input.GetKeyDown(reloadKey)) { weapon.reload(); }
    }

    void OnDrawGizmos() 
    {
        Debug.DrawRay(ray.origin, ray.direction);
    }

    void raycastShoot()
    {
        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, weapon.getMaxShootDist(), shootingMask))
        {
            Debug.Log("Shoot to: " + hitInfo.collider.gameObject.name);

            decalPool.enableObject(hitInfo.point + hitInfo.normal * zOffset, Quaternion.LookRotation(hitInfo.normal));
            Instantiate(decalParticles, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            if (hitInfo.collider.gameObject.TryGetComponent(out HealthSystem health))
            {
                health.takeDamage(weapon.getDamage());
            }
        }
        Instantiate(decalParticles, weaponShootingDummy.position, weaponShootingDummy.rotation);
    }

    void updateAmmoUI()
    {
        ammoUpdate.Invoke(weapon.getAmmoCurrentInside(), weapon.getAmmoAvailableStorage());
    }
}
