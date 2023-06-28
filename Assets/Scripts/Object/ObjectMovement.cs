using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : ObjectAbstract
{
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected Rigidbody2D baseRigid;
    protected Vector2 targetVelocity;
    [SerializeField] protected float hSpeed = 5f;
    [SerializeField] protected float vSpeed = 3f;
    protected bool facingRight = true;
    public bool CanMove => canMove;

    protected virtual void Update()
    {
        this.SetCanMove(!this.Controller.StateManager.isInteracting);
        this.HandleMovement();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    private void LoadRigidbody()
    {
        this.baseRigid = transform.parent.GetComponent<Rigidbody2D>();
    }
    protected virtual void HandleMovement()
    {
        if (canMove)
        {
            this.Move();

        }
        else
        {
            baseRigid.velocity = Vector2.zero;
            this.SetOnWalkAnimation(false);
        }
    }

    protected abstract void Move();

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.parent.localScale;
        scale.x *= -1;
        transform.parent.localScale = scale;
    }

    public void SetCanMove(bool param)
    {
        canMove = param;
    }

    protected virtual void SetOnWalkAnimation(bool active)
    {
        this.Controller.Animator.SetBool("isMoving", active);
    }
}
