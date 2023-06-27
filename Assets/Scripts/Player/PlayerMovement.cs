using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ObjectMovement
{

    
    [SerializeField] Rigidbody2D charRigid;

    [SerializeField] private bool isGrounded = true;

    

    [SerializeField] private Transform jumpDetector;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float fallingGravityScale;
    [SerializeField] private float jumpVal = 10f;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        this.HandleMovement();
        //this.HandleJump();
    }
    protected override void Move()
    {
        float hInput = InputManager.Instance.horizontalInput;
        float vInput = InputManager.Instance.verticalInput;

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
    protected virtual void HandleJump()
    {
        this.DetectGrounded();
        if (!InputManager.Instance.jumpingInput) return;

        if (isGrounded)
        {
            charRigid.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
            charRigid.gravityScale = jumpingGravityScale;
        }
        else
        {
            if (charRigid.velocity.y < 0)
            {
                charRigid.gravityScale = fallingGravityScale;
            }

            charRigid.velocity = new Vector2(targetVelocity.x, charRigid.velocity.y);
        }
    }



    private void DetectGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        this.isGrounded = hit.collider != null;
    }



    /*rivate void OnDrawGizmos()
    {
        Gizmos.DrawRay(jumpDetector.transform.position, -Vector3.up * detectionDistance);
    }*/
}
