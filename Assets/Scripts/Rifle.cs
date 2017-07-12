using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rifle : MonoBehaviour
{
    Weapon rifle = new Weapon();

    private void OnEnable()
    {
        rifle.ClipSize = 50;
        rifle.Damage = 5f;
        rifle.HitForce = Vector3.one * 2f;
        rifle.IsHitScanWeapon = true;
        rifle.FireRate = 0.1f;

        rifle.damageEventData = new DamageEventData(EventSystem.current);
        rifle.damageEventData.Initialize(rifle.Damage);

        rifle.OnEnable();
    }

    private void OnDisable()
    {
        rifle.OnDisable();
    }
}
