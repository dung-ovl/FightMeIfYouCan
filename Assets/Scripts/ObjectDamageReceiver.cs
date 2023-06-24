using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectDamageReceiver : DamageReceiver
{

    [SerializeField] private ObjectController objectController;

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

    protected void OnAnimationGetHit()
    {
        this.objectController.Animator.SetTrigger("GetHit");
    }

    protected void OnAnimationDead()
    {
        this.objectController.Animator.SetTrigger("Death");
    }

    public override void DeductHealthPoint(float hp)
    {
        base.DeductHealthPoint(hp);
        if (this.isDead)
        {
            this.OnDead();
        }
        
        this.OnAnimationGetHit();
    }

    protected override void OnDead()
    {
        this.OnAnimationDead();
        this.isTakeDamage = false;
        //this.objectController.DamageReceiver.gameObject.SetActive(false);
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
