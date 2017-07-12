using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pistol : MonoBehaviour
{
    private float fireRate = 1f;
    private float damage = 1f;
    private float lastShot = 0f;
    private Vector3 hitForce = Vector3.one;
    private int ammoCount = 10;
    private bool isHitScanWeapon = true;

    private DamageEventData damageEventData;

    private Ray ray;
    private RaycastHit hitInfo;

    private void Start()
    {
        damageEventData = new DamageEventData(EventSystem.current);
        damageEventData.Initialize(damage);
    }

    private void OnEnable()
    {
        PlayerController.onPlayerShoot += Shoot;
    }

    private void OnDisable()
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
        if (Time.time > fireRate + lastShot && ammoCount > 0)
        {
            ammoCount--;

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
