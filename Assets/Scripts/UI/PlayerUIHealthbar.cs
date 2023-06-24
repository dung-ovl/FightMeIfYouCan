using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHealthbar : DamageReceiverHealthBar
{
    protected override void Start()
    {
        this.LoadPlayerDamageReceiver();
    }

    private void LoadPlayerDamageReceiver()
    {
        if (damageReceiver != null) return;
        this.damageReceiver = GameManager.Instance.CurrentShip.GetComponent<PlayerController>().DamageReceiver;
    }
}
