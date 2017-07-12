using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDamageHandler : IEventSystemHandler
{
    void OnDamage(DamageEventData damageEventData);
}
