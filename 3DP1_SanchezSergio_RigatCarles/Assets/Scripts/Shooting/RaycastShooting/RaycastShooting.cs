using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastShooting : MonoBehaviour
{
    [SerializeField] DispersionCalculator dispersionCalculator;
    [Header("Shooting")]

    [SerializeField] List<Weapon> weapons;
    [SerializeField] Weapon.WeaponName currentWeapon;
    [SerializeField] LayerMask shootingMask;
    Weapon weapon;
    [SerializeField] Transform weaponDummy;
    [SerializeField] Transform weaponShootingDummy;
    Ray ray;

    [Header("Reloading")]
    [SerializeField] KeyCode reloadKey = KeyCode.R;

    [Header("VFX")]
    [SerializeField] ObjectPool hitImagePool;
    [SerializeField] ObjectPool hitParticlesPool;
    [SerializeField] ObjectPool shootParticlesPool;
    [SerializeField] float zOffset;

    [Header("UI")]
    [SerializeField] UnityEvent<int, int> ammoUpdate;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        findCurrentWeapon();
        updateAmmoUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            bool hasShooted = weapon.shoot();
            if(hasShooted) { raycastShoot(); updateAmmoUI(); playSoundShoot();}
            else { playSoundCantShoot(); }
        }
        if (Input.GetKeyDown(reloadKey)) 
        { 
            bool hasReloaded = weapon.reload(); 
            if (hasReloaded) { updateAmmoUI(); playSoundReload(); }
            else { playSoundCantReload(); }
        }
    }

    void findCurrentWeapon()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.getWeaponName() == currentWeapon) { this.weapon = weapon; break;}
        }
        weapon.gameObject.SetActive(true);
        weapon.transform.SetParent(weaponDummy);
        weapon.transform.position = weaponDummy.position;
        weapon.transform.rotation = weaponDummy.rotation;
    }

    void changeCurrentWeapon(Weapon.WeaponName weaponName)
    {
        if (weaponName != currentWeapon)
        {
            unSetPreviousWeapon();
            currentWeapon = weaponName;
            findCurrentWeapon();
        }
    }

    void unSetPreviousWeapon()
    {
        weapon.gameObject.SetActive(false);
        weapon.transform.SetParent(null);
    }

    void shoot()
    {
        playSoundShoot();
        shootParticlesPool.enableObject(weaponShootingDummy.position, weaponShootingDummy.rotation);

        RaycastHit hitInfo = raycastShoot();
        if (hitInfo.collider != null)
        {
            playSoundCollide();
            hitImagePool.enableObject(hitInfo.point + hitInfo.normal * zOffset, Quaternion.LookRotation(hitInfo.normal));
            hitParticlesPool.enableObject(hitInfo.point + hitInfo.normal * zOffset, Quaternion.LookRotation(hitInfo.normal));
            doDamage(hitInfo.collider.gameObject);
        }
    }

    RaycastHit raycastShoot()
    {
        ray = dispersionCalculator.calculateDispersion(weapon.getDispersion(), weapon.getMaxDistance());
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo, weapon.getMaxDistance(), shootingMask);
        return hitInfo;
    }

    void doDamage(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out HitCollider hitCollider))
            hitCollider.takeDamage(weapon.getDamage());
    }

    void updateAmmoUI()
    {
        ammoUpdate.Invoke(weapon.getAmmoCurrentInside(), weapon.getAmmoAvailableStorage());
    }

    void playSoundShoot() { audioSource.PlayOneShot(weapon.getSoundShoot()); }
    void playSoundCollide() { audioSource.PlayOneShot(weapon.getSoundCollide()); }
    void playSoundReload() { audioSource.PlayOneShot(weapon.getSoundReload()); }
    void playSoundCantShoot() { audioSource.PlayOneShot(weapon.getSoundCantShoot()); }
    void playSoundCantReload() { audioSource.PlayOneShot(weapon.getSoundCantReload()); }
}
