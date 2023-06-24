using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectDamageReceiver : DamageReceiver
{

    [SerializeField] protected ObjectController objectController;

    private float timeRemainOnDeath = 3f;

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


    protected override void OnDead()
    {
        this.OnAnimationDead();
        this.isTakeDamage = false;
        this.SetColliderActive(false);
        this.StartDeathEffect();
    }

    public void StartDeathEffect()
    {
        StartCoroutine(FlashDeadthCoroutine());
    }

    private IEnumerator FlashDeadthCoroutine()
    {

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
        GameObject.Destroy(transform.parent.gameObject);
    }
}
