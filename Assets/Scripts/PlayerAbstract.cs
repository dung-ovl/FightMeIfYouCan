using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbstract : GameMonoBehaviour
{
    [SerializeField] PlayerController controller;

    public PlayerController Controller => controller;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }

    private void LoadController()
    {
        this.controller = transform.parent.GetComponent<PlayerController>();
    }
}
