using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pistol : MonoBehaviour
{
    Weapon pistol = new Weapon();

    private void OnEnable()
    {
        pistol.ClipSize = 20;
        pistol.Damage = 1f;
        pistol.HitForce = Vector3.one;
        pistol.IsHitScanWeapon = true;
        pistol.FireRate = 0.25f;

        pistol.damageEventData = new DamageEventData(EventSystem.current);
        pistol.damageEventData.Initialize(pistol.Damage);

        pistol.OnEnable();
    }

    private void OnDisable()
    {
        pistol.OnDisable();
    }
}
