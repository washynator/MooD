using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    private float fireRate = 0f;
    private float damage = 0f;

    private int ammoCount = 0;

    private Vector3 hitForce = Vector3.zero;
    
    private Ray ray;

    private void OnEnable()
    {
        PlayerController.onPlayerShoot += Shoot;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerShoot -= Shoot;
    }

    private void Shoot(float _damage, float _fireRate, float _hitForce)
    {
        RaycastHit hitInfo;

        hitForce = Vector3.one * _hitForce;

        if (Camera.main != null)
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        }
        else
        {
            Debug.LogError("The scene does not contain a Camera with the tag \"MainCamera\", make sure to tag the Camera object!");
        }
        

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
            Debug.Log("The ray hit: " + hitInfo.transform.gameObject.name);

            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForceAtPosition(hitForce, hitInfo.point);
            }
        }
    }



}
