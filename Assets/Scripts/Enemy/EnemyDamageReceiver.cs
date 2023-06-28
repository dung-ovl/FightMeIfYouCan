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
        yield return new WaitForSeconds(knockDownTime/2);
        SoundManager.Instance.PlaySound("EnemyKnockdown");
        yield return new WaitForSeconds(knockDownTime/2);
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
        SoundManager.Instance.PlaySound("EnemyDeath");
        yield return base.FlashDeadthCoroutine();
        if (Random.Range(0f, 1f) <= 0.25f)
        {
            Transform itemDrop = ItemDropSpawner.Instance.Spawn("Meat", this.transform.parent.position, this.transform.parent.rotation);
            if (itemDrop != null)
            {
                itemDrop.gameObject.SetActive(true);
            }
        }
        EnemySpawner.Instance.Despawn(this.transform.parent);
    }

}
