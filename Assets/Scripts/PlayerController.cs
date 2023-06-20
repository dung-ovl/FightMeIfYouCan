using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameMonoBehaviour
{
    [SerializeField] Animator animator;

    public Animator Animator => animator;
    // Start is called before the first frame update
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    private void LoadAnimator()
    {
        this.animator = transform.GetComponentInChildren<Animator>();
    }
}
