using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class playerController : MonoBehaviour
{
    TouchingDirection touchingDirection;
    Vector2 moveInput;

    public float speed = 5f;
    private bool _isMoving = false;
    private bool _isAttacking = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.IsMoving, value);
        }
    }

    public bool IsAttacking
    {
        get { return _isAttacking; }
        private set
        {
            _isAttacking = value;
            animator.SetBool(AnimationStrings.IsAttacking, value);
        }
    }
    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;
    public bool _isFacingRight = true;
    public float jumpImpulse = 10f;

    public float CurrentMoveSpeed
    {
        get
        {
            if (canMove)
            {
                if ((IsMoving && !touchingDirection.IsOnWall))
                {
                    return speed;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }
    }
    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
        Vector2 attack = new Vector2(1, 0);

    }


    private void FixedUpdate()
    {
        if (!damageable.LockVelocity) { rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed * Time.fixedDeltaTime, rb.velocity.y); }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);


    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }
    private void SetFacingDirection(Vector2 vectorInput)
    {
        if (!IsAttacking)
        {
            if (moveInput.x > 0 && !isFacingRight) 
            {
                isFacingRight = true;
            }
            else if (moveInput.x < 0 && isFacingRight) 
            {
                isFacingRight = false;
            }
        }

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded && canMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !IsAttacking)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {

        rb.velocity = new Vector2(knockback.x, 1);
    }
    public void AttackAnimationStart()
    {
        IsAttacking = true;
    }
    public void AttackAnimationEnd()
    {
        IsAttacking = false;
    }
}
