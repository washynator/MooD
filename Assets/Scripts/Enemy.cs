using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageHandler
{
    private float hitPoints = 5f;
    private PlayerController player;

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
        player = FindObjectOfType<PlayerController>();
	}
	
	void FixedUpdate ()
	{
        transform.LookAt(player.transform);
        //transform.Translate(player.transform.position * Time.deltaTime * 0.5f);
        Vector3 movePosition = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(1.5f,1.5f,1.5f), Time.deltaTime * 1.5f);
        GetComponent<Rigidbody>().MovePosition(movePosition);
	}

}
