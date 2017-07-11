using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour, IDamageable
{
    public float hitPoints
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

    public void Damage(float damageAmount)
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
