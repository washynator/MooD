using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rifle : MonoBehaviour
{
    static Weapon rifle = new Weapon();
    public ParticleSystem shootingParticles;

    private void OnEnable()
    {
        rifle.ClipSize = 50;
        rifle.Damage = 0.25f;
        rifle.HitForce = Vector3.one * 2f;
        rifle.IsHitScanWeapon = true;
        rifle.FireRate = 0.1f;
        rifle.ShootingParticles = shootingParticles;

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
