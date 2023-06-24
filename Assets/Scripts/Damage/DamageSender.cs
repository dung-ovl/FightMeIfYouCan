using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : GameMonoBehaviour
{
    [SerializeField] private float damage = 2f;
    [SerializeField] private Vector3 hitPosOffset;

    public Vector3 HitPosOffset => hitPosOffset;
    public virtual void Send(Transform obj)
    {
        this.Send(obj, this.damage);
    }

    public virtual void Send(Transform obj, float damage)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        if (!damageReceiver.IsTakeDamage) return;
        this.Send(damageReceiver, damage);
        this.CreateImpactFX(damageReceiver.HitPos.position + hitPosOffset, damageReceiver.HitPos.rotation);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        this.Send(damageReceiver, this.damage);
    }

    public virtual void Send(DamageReceiver damageReceiver, float damage)
    {
        damageReceiver.DeductHealthPoint(damage);
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

    public virtual void SetHitPosOffset(Vector3 transform)
    {
        this.hitPosOffset = transform;
    }
}
