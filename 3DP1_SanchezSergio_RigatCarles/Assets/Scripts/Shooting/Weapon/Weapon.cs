using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponName { GUNTREE, PISTOL, SHOTGUN, RIFLE, SNIPER }
    [SerializeField] WeaponName weaponName;
    [SerializeField] GameObject weaponModel;
    [SerializeField] Animation weaponAnimation;

    [Header("Parameters")]
    [SerializeField] float damage = 5f;
    [SerializeField] float shootingRatio;
    [SerializeField] float maxDistance = 200f;
    [SerializeField] int ammoPerShot = 1;
    [SerializeField] float dispersion = 0.1f;

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

    public GameObject getWeaponModel() { return weaponModel; }

    public int getAmmoMaxInside() { return ammo_maxInside; }
    public int getAmmoCurrentInside() { return ammo_currentInside; }
    public int getAmmoAvailableStorage() { return ammo_availableStorage; }
    public AudioClip getSoundShoot() { return sound_shoot; }
    public AudioClip getSoundCollide() { return sound_collide; }
    public AudioClip getSoundReload() { return sound_reload; }
    public AudioClip getSoundCantShoot() { return sound_cantshoot; }
    public AudioClip getSoundCantReload() { return sound_cantreload; }
    public WeaponName getWeaponName() { return weaponName; }
    public float getMaxDistance() { return maxDistance; }
    public float getDispersion() { return dispersion; }

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

    public bool canReload() { return ammo_availableStorage > 0; }

    void animateShoot()
    {
        weaponAnimation.CrossFade("Shoot", 0.1f);
        weaponAnimation.CrossFadeQueued("Idle");
    }

    void animateReload()
    {
        weaponAnimation.CrossFade("Reload", 0.1f);
        weaponAnimation.CrossFadeQueued("Idle");
    }

    void animateCantShoot()
    {
        weaponAnimation.CrossFade("CantShoot", 0.1f);
        weaponAnimation.CrossFadeQueued("Idle");
    }

    void animateCantReload()
    {
        weaponAnimation.CrossFade("CantReload", 0.1f);
        weaponAnimation.CrossFadeQueued("Idle");
    }
    
}
