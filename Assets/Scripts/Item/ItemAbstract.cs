using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAbstract : GameMonoBehaviour
{
    [SerializeField] protected ItemController itemCtrl;
    public ItemController ItemCtrl { get => itemCtrl; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemCtrl();
    }

    protected virtual void LoadItemCtrl()
    {
        if (this.itemCtrl != null) return;
        this.itemCtrl = transform.parent.GetComponent<ItemController>();
        Debug.Log(transform.name + ": LoadItemCtrl", gameObject);
    }
}
