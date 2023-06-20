using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    // Start is called before the first frame update
    [SerializeField] protected float disLimit = 3f;
    [SerializeField] protected float distance = 0f;

    protected override bool CanDespawn()
    {
        Vector3 caremaPos = GameManager.Instance.CurrentShip.transform.position;
        caremaPos.z = 0;
        this.distance = Vector3.Distance(transform.position, caremaPos);
        if (this.distance > this.disLimit) return true;
        return false;
    }
}
