using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : GameMonoBehaviour
{
    [SerializeField] private float damage = 2f;
    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        this.CreateImpactFX(damageReceiver.HitPos.position, damageReceiver.HitPos.rotation);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.DeductHealthPoint(this.damage);
    }

    protected virtual void CreateImpactFX(Vector3 hitPos, Quaternion hitRot)
    {
        string fxImpactName = GetImpactFXName();
        Transform newFxImpact = FXSpawner.Instance.Spawn(fxImpactName, hitPos, hitRot);
        if (newFxImpact == null) return;
        newFxImpact.gameObject.SetActive(true);
    }

    protected virtual string GetImpactFXName()
    {
        return FXSpawner.Instance.Hit;
    }
}
