using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DamageEventData : BaseEventData
{
    public float Damage;
    public DamageEventData(EventSystem eventSystem) : base(eventSystem) { }

    public void Initialize(float damage)
    {
        Damage = damage;
    }

    public static ExecuteEvents.EventFunction<IDamageHandler> OnDamageHandler = delegate (IDamageHandler damageHandler, BaseEventData eventData)
    {
        DamageEventData castedData = ExecuteEvents.ValidateEventData<DamageEventData>(eventData);
        damageHandler.OnDamage(castedData);
    };
}
