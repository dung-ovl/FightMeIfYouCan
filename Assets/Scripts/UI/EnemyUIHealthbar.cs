using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIHealthbar : UIHealthbar
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        this.SetHealthBarActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentEnemy == null)
        {
            this.SetHealthBarActive(false);
            return;
        }
        this.SetHealthBarActive(true);
        this.UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        DamageReceiver damageReceiver = GameManager.Instance.CurrentEnemy.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.SetMaxHealth(damageReceiver.MaxHealthPoint);
        this.SetHealth(damageReceiver.HealthPoint);
    }
}
