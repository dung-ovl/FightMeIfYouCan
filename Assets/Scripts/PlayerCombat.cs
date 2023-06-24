using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH1,
    PUNCH2,
    PUNCH3,
    KICK1,
    KICK2,
}

public class PlayerCombat : PlayerAbstract
{
    private bool activateTimerToReset;

    private float defaultComboTimer = 0.4f;
    private float currentComboTimer;

    private ComboState currentComboState;

    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

    private void Update()
    {
        ComboAttack();
        SendDamage(currentComboState);
        ResetComboState();
    }

    private void ComboAttack()
    {
        if (InputManager.Instance.attackPunchInput)
        {
            if (currentComboState == ComboState.PUNCH3 || currentComboState == ComboState.KICK1 || currentComboState == ComboState.KICK2)
            {
                return;
            }

            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.PUNCH1)
            {
                Controller.Animator.SetTrigger("Punch1");
            }

            if (currentComboState == ComboState.PUNCH2)
            {
                Controller.Animator.SetTrigger("Punch2");
            }

            if (currentComboState == ComboState.PUNCH3)
            {
                Controller.Animator.SetTrigger("Punch3");
            }

            isAttacking = true;
            
        }

        if (InputManager.Instance.attackKickInput)
        {
            if (currentComboState == ComboState.KICK2 || currentComboState == ComboState.PUNCH3)
            {
                return;
            }
            if (currentComboState == ComboState.NONE || currentComboState == ComboState.PUNCH1 || currentComboState == ComboState.PUNCH2)
            {
                currentComboState = ComboState.KICK1;
            }
            else if (currentComboState == ComboState.KICK1)
            {
                currentComboState++;
            }

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.KICK1)
            {
                Controller.Animator.SetTrigger("Kick1");
            }

            if (currentComboState == ComboState.KICK2)
            {
                Controller.Animator.SetTrigger("Kick2");
            }

            isAttacking = true;
        }
    }

    private void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
                isAttacking = false;
            }
        }
    }

    private void SendDamage(ComboState comboState)
    {
        if (!isAttacking) return;
        List<DamageReceiver> damageReceivers = Controller.CombatTester.cols?.Select(item => item.GetComponent<DamageReceiver>()).ToList();
        foreach (var damageReceiver in damageReceivers)
        {
            if (damageReceiver != null)
            {
   
                float damage = 1;
                Vector3 hitOffSet = Vector3.zero;
                bool isKnockDown = false;
                switch (comboState)
                {
                    case ComboState.PUNCH1:
                        damage = 1;
                        hitOffSet.y += 0.5f;
                        break;
                    case ComboState.PUNCH2:
                        damage = 2;
                        hitOffSet.y += 0.5f;
                        break;
                    case ComboState.PUNCH3:
                        damage = 3;
                        hitOffSet.y += 0.6f;
                        isKnockDown = true;
                        break;
                    case ComboState.KICK1:
                        damage = 2;
                        hitOffSet.y -= 0.2f;
                        break;
                    case ComboState.KICK2:
                        damage = 3;
                        hitOffSet.y += 0.8f;
                        break;
                    default: break;
                }
                this.Controller.DamageSender.SetHitPosOffset(hitOffSet);
                this.Controller.DamageSender.SetDamage(damage);
                this.Controller.DamageSender.Send(damageReceiver.transform);
                
                if (isKnockDown && !damageReceiver.IsDead)
                {
                    StartCoroutine(damageReceiver.KnockDown());
                    
                }
                GameManager.Instance.SetCurrentEnemy(damageReceiver.transform);
                isAttacking = false;
            }
        }
     
    }
}
