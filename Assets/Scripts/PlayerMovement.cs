using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [SerializeField] private float hSpeed = 5f;
    [SerializeField] private float vSpeed = 3f;
    [SerializeField] Rigidbody2D baseRigid;
    [SerializeField] Rigidbody2D charRigid;
    [SerializeField] bool canMove = true;

    private bool isJumping = false;
    [SerializeField] private bool isGrounded = true;
    private bool facingRight = true;
    private Vector2 targetVelocity;

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
    protected void Update()
    {
        this.HandleMovement();
        //this.HandleJump();
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.parent.localScale;
        scale.x *= -1;
        transform.parent.localScale = scale;
    }

    private void DetectGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        this.isGrounded = hit.collider != null;
    }

    protected virtual void SetOnWalkAnimation(bool active)
    {
        this.Controller.Animator.SetBool("isMoving", active);
    }

    /*rivate void OnDrawGizmos()
    {
        Gizmos.DrawRay(jumpDetector.transform.position, -Vector3.up * detectionDistance);
    }*/
}
