using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageReceiver : ObjectDamageReceiver
{
    private float knockDownTime = 2f;
    
    protected void OnAnimationKnockDown()
    {
        this.objectController.Animator.SetTrigger("KnockDown");
    }

    protected void OnAnimationStandUp()
    {
        this.objectController.Animator.SetTrigger("StandUp");
    }

    public void OnAnimationGetHit()
    {
        this.objectController.Animator.SetTrigger("GetHit");
    }

    public override IEnumerator KnockDown()
    {
        this.OnAnimationKnockDown();
        this.SetColliderActive(false);
        yield return new WaitForSeconds(knockDownTime);
        this.StandUp();
    }

   


    public void StandUp()
    {
        this.OnAnimationStandUp();
        this.SetColliderActive(true);
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
}
