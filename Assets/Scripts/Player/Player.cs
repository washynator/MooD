using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IDamageHandler
{
    [SerializeField]
    private float hitPoints = 100f;

    public float HitPoints
    {
        get
        {
            return hitPoints;
        }

        set
        {
            hitPoints = value;

            if (hitPoints <= 0f)
            {
                hitPoints = 0f;
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
        gameObject.GetComponent<Player>().enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
    }

    void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

}
