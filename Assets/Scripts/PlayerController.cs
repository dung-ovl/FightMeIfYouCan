using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameMonoBehaviour
{
    [SerializeField] Animator animator;

    public Animator Animator => animator;

    [SerializeField] CombatTester combatTester;

    public CombatTester CombatTester => combatTester;

    [SerializeField] DamageSender damageSender;

    public DamageSender DamageSender => damageSender;
    // Start is called before the first frame update
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadCombatTester();
        this.LoadDamageSender();
    }

    private void LoadDamageSender()
    {
        this.damageSender = this.GetComponentInChildren<DamageSender>();
    }

    private void LoadCombatTester()
    {
        this.combatTester = this.GetComponentInChildren<CombatTester>();
    }

    private void LoadAnimator()
    {
        this.animator = transform.GetComponentInChildren<Animator>();
    }
}
