using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyBoxController : MonoBehaviour
{
    public float walkSpeed = 2f;
    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    public DetectionZone attackZone;
    Animator animator;
    Damageable damageable;
    public DetectionZone detectionZone;



    public float jumpForce = 0.52f;

    public float timeBetweenJump = 16f;
    private float nextJumpTime;
    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set { _hasTarget = value; animator.SetBool(AnimationStrings.hasTarget, value); } }
    private float timeSinceDetection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        nextJumpTime = Time.time + timeBetweenJump;
        timeSinceDetection = Mathf.Infinity;
        
    }

    void Update()
    {
        
        if (HasTarget)
        {
                Attack();
        }
    }
    private void FixedUpdate()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (HasTarget)
        {
            timeSinceDetection = Time.time;
        }
    }


    public void OnHit(int damage, Vector2 knockback)
    {

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void Attack()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            if (Time.time >= nextJumpTime + 1f) { rb.velocity = new Vector2(0.7f, 1f); nextJumpTime = Time.time + timeBetweenJump; }
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            if (Time.time >= nextJumpTime +1f) { rb.velocity = new Vector2(-0.7f, 1f); nextJumpTime = Time.time + timeBetweenJump; }
        }



    }

}
