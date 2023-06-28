using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 distanceRange;

    protected override void Move()
    {
        if (target == null) return;
        Vector2 vectorTarget = (target.position - transform.parent.position);

        if (Math.Abs(vectorTarget.x) <= distanceRange.x)
        {
            vectorTarget.x = 0;
        }

        if (Math.Abs(vectorTarget.y) <= distanceRange.y)
        {
            vectorTarget.y = 0;
        }
        Vector2 vectorTargetNol = vectorTarget.normalized;
        float hInput = vectorTargetNol.x;
        float vInput = vectorTargetNol.y;

        targetVelocity = new Vector2(hInput * hSpeed, vInput * vSpeed);

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
