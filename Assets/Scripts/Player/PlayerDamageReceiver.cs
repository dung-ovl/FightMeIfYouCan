using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : ObjectDamageReceiver
{
    protected override IEnumerator FlashDeadthCoroutine()
    {
        SoundManager.Instance.PlaySound("PlayerDeath");
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(transform.parent.gameObject);
    }

    public override void DeductHealthPoint(float hp)
    {
        base.DeductHealthPoint(hp);
        SoundManager.Instance.PlaySound("PlayerHurt");
    }
}
