using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float hitPoints { get; set; }
    void Damage(float damageAmount);
}
