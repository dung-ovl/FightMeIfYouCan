using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLooter : ObjectAbstract
{

    [SerializeField] private Vector3 overlapSize = Vector3.one;

    [SerializeField] private LayerMask itemLayer;

    [SerializeField] private List<Collider2D> cols = new List<Collider2D>();

    public bool HasItemCanPickup = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();

    }

    private void Update()
    {
        this.GetContactCollider();
        this.PickUpItem();
        this.CheckHasItemCanPickup();
    }

    private void PickUpItem()
    {
        if (InputManager.Instance.pickUpInput)
        {
            if (cols.Count > 0)
            {
                ItemPickupable itemPickupable = cols[0].GetComponent<ItemPickupable>();
                if (itemPickupable == null) return;
                ItemCode itemCode = itemPickupable.ItemCtrl.ItemProfileSO.itemCode;
                Debug.Log("Picked " + itemCode.ToString());
                this.Controller.StateManager.SetState(ObjectState.Pickup);
                this.OnAnimationPickup();
                if (itemCode == ItemCode.MeatItem)
                {
                    this.Controller.DamageReceiver.AddHealthPoint(2f);
                }
                SoundManager.Instance.PlaySoundOneShot("Pickup", 0.3f);
                itemPickupable.Picked();
            }
        }
    }

    protected void OnAnimationPickup()
    {
        this.Controller.Animator.SetTrigger("Pickup");
    }

    private void GetContactCollider()
    {
        cols = Physics2D.OverlapBoxAll(transform.position, overlapSize / 2f, 0, itemLayer).ToList();
    }

    private void CheckHasItemCanPickup()
    {
        if (cols.Count > 0)
        {
            HasItemCanPickup = true;
        }
        else
        {
            HasItemCanPickup = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, overlapSize);
    }
}
