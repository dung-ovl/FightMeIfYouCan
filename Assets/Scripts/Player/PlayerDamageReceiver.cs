using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        Debug.Log("Dead");
    }
}
