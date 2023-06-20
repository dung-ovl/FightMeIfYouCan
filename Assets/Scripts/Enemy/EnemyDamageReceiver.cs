using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        this.OnDeadFX();
        //this.enemyController.EnemyDespawn.DespawnObject();
    }

    protected virtual void OnDeadFX()
    {
        string fxOnDeadName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxOnDeadName, transform.position, transform.rotation);
        if (fxOnDead == null) return;
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetOnDeadFXName()
    {
        return this.onDeadFXName;
    }
}
