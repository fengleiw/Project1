using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class pigScript : MonoBehaviour
{
    public float walkSpeed = 2f;
    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    public DetectionZone attackZone;
    Animator animator;
    Damageable damageable;
    public DetectionZone detectionZone;
    public DetectionZone cliffDetectionZone;
    public enum WalkableDirection { Right, Left }
    public float walkStopRate = 0.6f;

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // flip
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set { _hasTarget = value; animator.SetBool(AnimationStrings.hasTarget, value); } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (walkSpeed == 0)
        {
            animator.SetBool("move", false);
        }
        else
        {
            animator.SetBool("move", true);
            if (touchingDirection.IsGrounded && touchingDirection.IsOnWall || cliffDetectionZone.detectedColliders.Count == 0)
            {
                FlipDirection();
            }
            if (!damageable.LockVelocity)
            {
                if (canMove) { rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y); }
                else
                    rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }

    }


    private void FlipDirection()
    {
        if (walkDirection == WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        }
        else if (walkDirection == WalkableDirection.Left)
        {
            walkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    void Start()
    {

    }
    public void OnHit(int damage, Vector2 knockback)
    {

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }



}
