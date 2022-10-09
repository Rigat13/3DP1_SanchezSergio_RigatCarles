using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponType", menuName = "Weapons/Create Weapon")]
public class WeaponType : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] GameObject weaponModel;
    [SerializeField] Animation weaponAnimation;

    [Header("Parameters")]
    [SerializeField] float damage = 5f;
    [SerializeField] float shootingRatio;
    [SerializeField] float maxShootDist = 200f;
    [SerializeField] int ammoPerShot = 1;

    [Header("Ammo")]
    [SerializeField] int ammo_maxInside;
    [SerializeField] int ammo_currentInside;
    [SerializeField] int ammo_availableStorage;

    [Header("Audio")]
    [SerializeField] AudioClip sound_shoot;
    [SerializeField] AudioClip sound_collide;
    [SerializeField] AudioClip sound_reload;
    [SerializeField] AudioClip sound_cantshoot;
    [SerializeField] AudioClip sound_cantreload;

    public float getDamage() { return damage; }

    public float getMaxShootDist() { return maxShootDist; }
    public GameObject getWeaponModel() { return weaponModel; }

    public int getAmmoMaxInside() { return ammo_maxInside; }
    public int getAmmoCurrentInside() { return ammo_currentInside; }
    public int getAmmoAvailableStorage() { return ammo_availableStorage; }
    public AudioClip getSoundShoot() { return sound_shoot; }
    public AudioClip getSoundCollide() { return sound_collide; }
    public AudioClip getSoundReload() { return sound_reload; }
    public AudioClip getSoundCantShoot() { return sound_cantshoot; }
    public AudioClip getSoundCantReload() { return sound_cantreload; }

    public bool shoot()
    {
        if (canShoot(ammoPerShot))
        {
            decreaseAmmo(ammoPerShot);
            animateShoot();
            return true;
        }
        else
        {
            animateCantShoot();
            return false;
        }
    }

    public bool canShoot(int ammoRequired) { return ammo_currentInside >= ammoRequired; }
    public bool canShoot() { return canShoot(ammoPerShot); }

    public void decreaseAmmo(int amount)
    {
        ammo_currentInside -= amount;
    }

    public bool reload()
    {
        if (canReload())
        {
            int ammo_SpaceLeft = ammo_maxInside - ammo_currentInside;
            int ammo_ToReload = enoughAmmoToReload(ammo_SpaceLeft) ? ammo_SpaceLeft : ammo_availableStorage;

            ammo_availableStorage -= ammo_ToReload;
            ammo_currentInside += ammo_ToReload;

            animateReload();
            return true;
        }
        else
        {
            animateCantReload();
            return false;
        }
    }

    private bool enoughAmmoToReload(int ammo_SpaceLeft) { return ammo_availableStorage >= ammo_SpaceLeft; }

    public bool canReload() { return ammo_availableStorage >= 0; }

    void animateShoot()
    {
        weaponAnimation.CrossFade("Shoot", 0.1f);
        weaponAnimation.CrossFadeQueued("Idle");
        Debug.Log("Shoot");
        Debug.Log("Bullets in gun: " + ammo_currentInside+ " Bullets outside: " + ammo_availableStorage+ " Bullets max: " + ammo_maxInside);
    }

    void animateReload()
    {
        Debug.Log("Reload");
        Debug.Log("Bullets in gun: " + ammo_currentInside+ " Bullets outside: " + ammo_availableStorage+ " Bullets max: " + ammo_maxInside);
    }

    void animateCantShoot()
    {
        Debug.Log("Cant Shoot");
        Debug.Log("Bullets in gun: " + ammo_currentInside+ " Bullets outside: " + ammo_availableStorage+ " Bullets max: " + ammo_maxInside);
    }

    void animateCantReload()
    {
        Debug.Log("Cant Reload");
        Debug.Log("Bullets in gun: " + ammo_currentInside+ " Bullets outside: " + ammo_availableStorage+ " Bullets max: " + ammo_maxInside);
    }
    
}
