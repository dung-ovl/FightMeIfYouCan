using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectController
{
    [SerializeField] protected ItemLooter itemLooter;

    public ItemLooter ItemLooter => itemLooter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemLooter();
    }

    private void LoadItemLooter()
    {
        this.itemLooter = GetComponentInChildren<ItemLooter>();
    }
}
