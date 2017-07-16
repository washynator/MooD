using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IDamageHandler
{
    public float HitPoints
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void OnDamage(DamageEventData damageAmount)
    {
        throw new NotImplementedException();
    }

    void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

}
