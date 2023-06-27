using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    [SerializeField] private Transform target;

    protected override void Move()
    {

        Vector2 vectorTarget = (target.position - transform.parent.position).normalized;

        float hInput = vectorTarget.x;
        float vInput = vectorTarget.y;

        targetVelocity = new Vector2(vectorTarget.x * hSpeed, vectorTarget.y * vSpeed);

        baseRigid.velocity = targetVelocity;

        bool isMoving = targetVelocity.magnitude != 0;
        this.SetOnWalkAnimation(isMoving);

        if (hInput > 0 && !facingRight)
        {
            this.Flip();
        }
        else if (hInput < 0 && facingRight)
        {
            this.Flip();
        }
    }

    protected override void Start()
    {
        this.target = GameManager.Instance.CurrentShip.transform;
    }

}
