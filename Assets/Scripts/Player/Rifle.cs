using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rifle : MonoBehaviour
{
    static Weapon rifle = new Weapon();
    public GameObject impactParticles;

    private void OnEnable()
    {
        rifle.ClipSize = 250;
        rifle.Damage = 0.5f;
        rifle.HitForce = Vector3.one * 10f;
        rifle.IsHitScanWeapon = true;
        rifle.FireRate = 0.125f;
        rifle.CurrentWeapon = this.gameObject;
        rifle.ImpactEffect = impactParticles;

        rifle.damageEventData = new DamageEventData(EventSystem.current);
        rifle.damageEventData.Initialize(rifle.Damage);

        rifle.OnEnable();
    }

    public static string CheckAmmo()
    {
        return rifle.ClipSize.ToString();
    }

    public static void Reload()
    {
        rifle.ClipSize = 50;
    }

    private void OnDisable()
    {
        rifle.OnDisable();
    }
}
