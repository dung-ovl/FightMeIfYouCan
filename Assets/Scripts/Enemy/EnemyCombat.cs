using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCombat : EnemyAbstract
{
    [SerializeField] private float attackDelay = 2f;


    private bool isAttacking = false;
    private bool canAttack = true;

    protected override void Start()
    {
        this.InvokeRepeating("StartAttack", 0f, 5f);
    }

    private void Update()
    {
        if (this.Controller.StateManager.isInteracting)
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
        this.SendDamage();
    }

    public void StartAttack()
    {
        StartCoroutine(AttackAfterDelay());
    }

    private IEnumerator AttackAfterDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        this.Attack();
    }

    private void Attack()
    {
        if (!this.canAttack) return;
        this.Controller.Animator.SetTrigger("Punch1");
        this.Controller.StateManager.SetState(ObjectState.Attack);
        this.isAttacking = true;
    }

    private void SendDamage()
    {
        if (!isAttacking)
        {
            return;
        }
        List<DamageReceiver> damageReceivers = Controller.CombatTester.cols?.Select(item => item.GetComponent<DamageReceiver>()).ToList();
        foreach (var damageReceiver in damageReceivers)
        {
            if (damageReceiver != null)
            {

                float damage = 1;
                Vector3 hitOffSet = Vector3.zero;
                this.Controller.DamageSender.SetHitPosOffset(hitOffSet);
                this.Controller.DamageSender.SetDamage(damage);
                this.Controller.DamageSender.Send(damageReceiver.transform);
                isAttacking = false;
            }
        }
    }
}
