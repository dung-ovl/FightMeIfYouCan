using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiverHealthBar : UIHealthbar
{
    [SerializeField] protected DamageReceiver damageReceiver;

    private void Update()
    {
        UpdateHealhbar();
    }
    private void UpdateHealhbar()
    {
        if (this.damageReceiver == null) return;
        this.SetMaxHealth(damageReceiver.MaxHealthPoint);
        this.SetHealth(damageReceiver.HealthPoint);
    }

    public void SetDamageReceiver(DamageReceiver damageReceiver)
    {
        this.damageReceiver = damageReceiver;
    }
}
