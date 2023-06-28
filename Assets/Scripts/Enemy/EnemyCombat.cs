using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyCombat : EnemyAbstract
{
    [SerializeField] private float attackDelay = 1.5f;
    [SerializeField] private float attackTimer = 0f;
    [SerializeField] private Vector2 attackRange;
    [SerializeField] private Transform attackTarget;

    private bool isAttacking = false;
    private bool canAttack = true;

    protected override void Start()
    {
        base.Start();
        attackTarget = GameManager.Instance.CurrentShip;
    }

    private void Update()
    {
        this.SendDamage();
        this.CheckCanAttack();
        this.AttackTargetOnDistance();
    }

    private void CheckCanAttack()
    {
        if (this.Controller.StateManager.isInteracting)
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    private void AttackTargetOnDistance()
    {
        if (attackTarget == null) return;
        Vector2 vectorTarget = (attackTarget.position - transform.parent.position);

        if (Math.Abs(vectorTarget.x) <= attackRange.x && Math.Abs(vectorTarget.y) <= attackRange.y)
        {
            this.attackTimer += Time.deltaTime;
            if (this.attackTimer < attackDelay) return;
            this.Attack();
            attackTimer = 0f;
        }   
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
                SoundManager.Instance.PlaySound("Punch1");
                isAttacking = false;
            }
        }
    }
}
