using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectDamageReceiver : DamageReceiver
{

    [SerializeField] protected ObjectController objectController;

    private float timeRemainOnDeath = 2f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }

    private void LoadController()
    {
        this.objectController = this.transform.parent.GetComponent<ObjectController>();
    }


    protected void OnAnimationDead()
    {
        this.objectController.Animator.SetTrigger("Death");
    }

    public void OnAnimationGetHit()
    {
        this.objectController.Animator.SetTrigger("GetHit");
    }

    protected override void OnDead()
    {
        this.OnAnimationDead();
        this.isTakeDamage = false;
        this.SetColliderActive(false);
        this.objectController.StateManager.SetState(ObjectState.Die);
        this.StartDeathEffect();
    }

    public void StartDeathEffect()
    {
        StartCoroutine(FlashDeadthCoroutine());
    }

    protected virtual IEnumerator FlashDeadthCoroutine()
    {
        if (GameManager.Instance.CurrentEnemy != null && GameManager.Instance.CurrentEnemy == this.transform.parent)
        {
            GameManager.Instance.SetCurrentEnemy(null);
        }
        bool isVisible = true;
        float deathFlashTimer = 0f;
        yield return new WaitForSeconds(1f);

        while (deathFlashTimer < timeRemainOnDeath)
        {
            deathFlashTimer += 0.1f;
            this.objectController.Sprite.enabled = isVisible;
            isVisible = !isVisible;
            yield return new WaitForSeconds(0.1f);
        }

    }

    public override void DeductHealthPoint(float hp)
    {
        base.DeductHealthPoint(hp);
        if (this.isDead)
        {
            this.OnDead();
            return;
        }
        this.OnAnimationGetHit();
        this.objectController.StateManager.SetState(ObjectState.Hurt);
    }

    protected override void Reborn()
    {
        base.Reborn();
        this.objectController.Sprite.enabled = true;
    }
}
