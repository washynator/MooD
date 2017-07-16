using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pistol : MonoBehaviour
{
    static Weapon pistol = new Weapon();
    public GameObject impactParticles;

    private void OnEnable()
    {
        pistol.ClipSize = 10;
        pistol.Damage = 2f;
        pistol.HitForce = Vector3.one * 0.35f;
        pistol.IsHitScanWeapon = true;
        pistol.FireRate = 0.5f;
        pistol.CurrentWeapon = this.gameObject;
        pistol.ImpactEffect = impactParticles;

        pistol.damageEventData = new DamageEventData(EventSystem.current);
        pistol.damageEventData.Initialize(pistol.Damage);

        pistol.OnEnable();
    }

    public static string CheckAmmo()
    {
        return pistol.ClipSize.ToString();
    }

    public static void Reload()
    {
        pistol.ClipSize = 10;
    }

    private void OnDisable()
    {
        pistol.OnDisable();
    }
}