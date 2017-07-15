using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon
{
    public Weapon(float _fireRate, float _damage, Vector3 _hitForce, int _clipSize, bool _isHitScanWeapon, GameObject _currentWeapon, GameObject _impactEffect)
    {
        fireRate = _fireRate;
        damage = _damage;
        hitForce = _hitForce;
        clipSize = _clipSize;
        isHitScanWeapon = _isHitScanWeapon;
        currentWeapon = _currentWeapon;
        impactEffect = _impactEffect;
    }

    public Weapon()
    {
        
    }

    private float fireRate = 1f;
    private float damage = 1f;
    private Vector3 hitForce = Vector3.one;
    private int clipSize = 10;
    private bool isHitScanWeapon = true;
    private GameObject currentWeapon;
    private GameObject impactEffect;

    public DamageEventData damageEventData;

    private float lastShot = 0f;
    private Ray ray;
    private RaycastHit hitInfo;

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public float FireRate
    {
        get
        {
            return fireRate;
        }

        set
        {
            fireRate = value;
        }
    }

    public Vector3 HitForce
    {
        get
        {
            return hitForce;
        }

        set
        {
            hitForce = value;
        }
    }

    public int ClipSize
    {
        get
        {
            return clipSize;
        }

        set
        {
            clipSize = value;
        }
    }

    public bool IsHitScanWeapon
    {
        get
        {
            return isHitScanWeapon;
        }

        set
        {
            isHitScanWeapon = value;
        }
    }

    public GameObject CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            currentWeapon = value;
        }
    }

    public GameObject ImpactEffect
    {
        get
        {
            return impactEffect;
        }

        set
        {
            impactEffect = value;
        }
    }

    private void Start()
    {
        //damageEventData = new DamageEventData(EventSystem.current);
        //damageEventData.Initialize(damage);
    }

    public void OnEnable()
    {
        PlayerController.onPlayerShoot += Shoot;
    }

    public void OnDisable()
    {
        PlayerController.onPlayerShoot -= Shoot;
    }

    private void Shoot()
    {
        damageEventData.Initialize(damage);

        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Time.time > fireRate + lastShot && clipSize > 0)
        {
            clipSize--;

            if (Physics.Raycast(CheckRay(), out hitInfo, Mathf.Infinity) && isHitScanWeapon == true)
            {
                lastShot = Time.time;

                if (currentWeapon != null)
                {
                    ParticleSystem particleSystem = currentWeapon.GetComponentInChildren<ParticleSystem>();
                    AudioSource weaponSound = currentWeapon.GetComponent<AudioSource>();
                    Animator weaponAnimation = currentWeapon.GetComponent<Animator>();

                    if (weaponSound != null)
                    {
                        weaponSound.Play();
                    }

                    if (weaponAnimation != null)
                    {
                        weaponAnimation.SetTrigger("Shoot");
                    }

                    if (particleSystem != null)
                    {
                        particleSystem.Play();
                    }
                    else
                    {
                        Debug.LogError("The CurrentWeapons ParticleSystem is null, add it in the Inspector!");
                    }
                }
                else
                {
                    Debug.LogError("The CurrentWeapon GameObject is empty, check that your weapon sets the CurrentWeapon GameObject in OnEnable()");
                }

                ExecuteEvents.ExecuteHierarchy(hitInfo.collider.gameObject, damageEventData, DamageEventData.OnDamageHandler);

                // TODO: Add the impactEffect particle system to the weapon and instantiate it somehow at the hitInfo.point
                if (impactEffect != null)
                {
                    GameObject impactEffectGO = GameObject.Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    GameObject.Destroy(impactEffectGO, 0.35f);
                }
                

                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForceAtPosition(hitForce, hitInfo.point, ForceMode.Impulse);
                }
            }
            else
            {
                lastShot = Time.time;
                Debug.Log("Now you are shooting a projectile weapon");
            }
        }
    }

    private Ray CheckRay()
    {
        if (Camera.main != null)
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        }
        else
        {
            Debug.LogError("The scene does not contain a Camera with the tag \"MainCamera\", make sure to tag the Camera object!");
        }

        return ray;
    }
}
