using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageHandler
{
    private float hitPoints = 5f;
    private PlayerController player;
    private NavMeshAgent navMeshAgent;
    private float lastChecked = 0f;
    private float checkRate = 0.2f;

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
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = player.transform.position;
	}

	void Update ()
	{
        if (Time.time > lastChecked + checkRate)
        {
            lastChecked = Time.time;
            UpdatePlayerPosition();
        }
	}

    private void UpdatePlayerPosition()
    {
        navMeshAgent.destination = player.transform.position;
    }

}
