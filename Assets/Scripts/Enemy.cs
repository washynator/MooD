using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageHandler
{
    private float hitPoints = 5f;

    public float HitPoints
    {
        get
        {
            return hitPoints;
        }

        set
        {
            hitPoints = value;

            if (hitPoints <= 0)
            {
                Die();
            }
        }
    }

    public void OnDamage(DamageEventData damageAmount)
    {
        HitPoints -= damageAmount.Damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

}
