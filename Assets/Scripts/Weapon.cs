using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon
{
    public Weapon(float _fireRate, float _damage, Vector3 _hitForce, int _clipSize, bool _isHitScanWeapon)
    {
        fireRate = _fireRate;
        damage = _damage;
        hitForce = _hitForce;
        clipSize = _clipSize;
        isHitScanWeapon = _isHitScanWeapon;
    }

    public Weapon()
    {
        
    }

    private float fireRate = 1f;
    private float damage = 1f;
    private Vector3 hitForce = Vector3.one;
    private int clipSize = 10;
    private bool isHitScanWeapon = true;

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

                ExecuteEvents.ExecuteHierarchy(hitInfo.collider.gameObject, damageEventData, DamageEventData.OnDamageHandler);

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
