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

    public override IEnumerator KnockDown()
    {
        this.objectController.StateManager.SetState(ObjectState.KnockDown);
        this.OnAnimationKnockDown();
        this.SetColliderActive(false);
        yield return new WaitForSeconds(knockDownTime);
        StartCoroutine(this.StandUp());
        
    }

    public IEnumerator StandUp()
    {
        this.objectController.StateManager.SetState(ObjectState.StandUp);
        this.OnAnimationStandUp();
        yield return new WaitForSeconds(1f);
        this.SetColliderActive(true);
    }

    protected override IEnumerator FlashDeadthCoroutine()
    {
        yield return base.FlashDeadthCoroutine();
        EnemySpawner.Instance.Despawn(this.transform.parent);
    }    

}
