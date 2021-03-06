﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDamageHandler
{
    public enum EnemyState
    {
        Patrolling,
        Attacking,
        Chasing,
        Dead
    }

    public EnemyState enemyState;
    private float hitPoints = 5f;
    private Player player;
    private NavMeshAgent navMeshAgent;
    private float lastChecked = 0f;
    private float checkRate = 0.2f;

    private float fieldOfView = 60f;
    private float lineOfSightDistance = 150f;

    private RaycastHit hitInfo;
    private DamageEventData damageEventData;

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
        enemyState = EnemyState.Dead;
        Destroy(gameObject);
    }

    void Start ()
	{
        enemyState = EnemyState.Patrolling;
        player = FindObjectOfType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = LevelGenerator.Instance.GetWaypoint(Random.Range(0, LevelGenerator.Instance.wayPoints.Count - 1));
    }

	void Update ()
	{
        LookForPlayer();

        if (Time.time > lastChecked + checkRate)
        {
            lastChecked = Time.time;

            switch(enemyState)
            {
                // TODO: the check for attack distance is buggy, also the enemy faces weird directions
                case EnemyState.Patrolling:
                    if (CheckIfPathIsComplete() == true)
                    {
                        navMeshAgent.destination = LevelGenerator.Instance.GetWaypoint(Random.Range(0, LevelGenerator.Instance.wayPoints.Count - 1));
                    }
                    break;
                case EnemyState.Chasing:
                    navMeshAgent.destination = player.transform.position;

                    if (CheckIfPathIsComplete() == true)
                    {
                        enemyState = EnemyState.Attacking;
                    }
                    else
                    {
                        enemyState = EnemyState.Chasing;
                    }

                    break;
                case EnemyState.Attacking:
                    if (player != null)
                    {
                        navMeshAgent.destination = player.transform.position;

                        if (player.enabled == false)
                        {
                            enemyState = EnemyState.Patrolling;
                        }
                    }

                    if (CheckIfPathIsComplete() == true)
                    {
                        Attack(5f);
                    }
                    else
                    {
                        enemyState = EnemyState.Chasing;
                    }

                    break;
                case EnemyState.Dead:
                    break;
            }
        }
	}

    private void LookForPlayer()
    {
        if (player != null)
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            float angle = Vector3.Angle(playerDirection, transform.forward);

            if (angle < fieldOfView * 0.5f)
            {
                if (Physics.Raycast(transform.position + transform.up + transform.forward, playerDirection.normalized, out hitInfo, lineOfSightDistance))
                {
                    // TODO: add a timer that checks how long ago the player was seen, change state accordingly
                    if (hitInfo.transform.GetComponent<Player>() != null)
                    {
                        enemyState = EnemyState.Chasing;
                        Debug.Log(enemyState);
                    }
                    else
                    {
                        enemyState = EnemyState.Patrolling;
                        Debug.Log(enemyState);
                    }
                }
            }
        }
    }

    private bool CheckIfPathIsComplete()
    {
        if (Vector3.Distance(navMeshAgent.destination, navMeshAgent.transform.position) <= navMeshAgent.stoppingDistance)
        {
            if (navMeshAgent.hasPath == false || navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void Attack(float _damage)
    {
        print("Attacking... and doing " + _damage + " points of damage");
        damageEventData = new DamageEventData(EventSystem.current);
        damageEventData.Initialize(_damage);

        player.OnDamage(damageEventData);
    }

}
