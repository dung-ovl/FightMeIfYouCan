using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    // Start is called before the first frame update
    [SerializeField] protected float disLimit = 20f;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected Transform target;

    protected override void Start()
    {
        LoadTarget();
    }

    private void LoadTarget()
    {
        this.target = GameManager.Instance.MainCamera.transform;
    }

    protected override bool CanDespawn()
    {
        Vector3 pos = target.position;
        pos.z = 0;
        this.distance = Vector3.Distance(transform.position, pos);
        if (this.distance > this.disLimit) return true;
        return false;
    }
}
