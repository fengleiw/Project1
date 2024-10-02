using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombController : MonoBehaviour
{
    ProjectileLauncher projectileLauncher;
    Rigidbody2D rb;
    public DetectionZone attackZone;
    Animator animator;
    Damageable damageable;
    public DetectionZone detectionZone;
    Projectile projectile;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileLauncher = GetComponent<ProjectileLauncher>();
        attackZone = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        detectionZone = GetComponent<DetectionZone>();
        projectile = GetComponent<Projectile>();
        
    }
    private void Start()
    {
        
    }
    public bool hasTarget1;
    void Update()
    {
        if (hasTarget1)
        {
            animator.SetBool("hasTarget1", true);
        }
        else
        { animator.SetBool("hasTarget1", false); }
        
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasTarget1 = true;
        } 
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            hasTarget1 = false;
        }
    }
            //private void FixedUpdate()
            //{
            //    if (hasTarget1)
            //    {
            //        animator.SetBool("hasTarget1", true);

            //    }
            //    else
            //        animator.SetBool("hasTarget1", false);
            //}
            //private void FixedUpdate()
            //{
            //    if (touchingDirection.IsGrounded && touchingDirection.IsOnWall || cliffDetectionZone.detetedColliders.Count == 0)
            //    {
            //        FlipDirection();
            //    }
            //    if (!damageable.LockVelocity)
            //    {
            //        if (canMove) { rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y); }
            //        else
            //            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);

            //    }
            //}


            //private void FlipDirection()
            //{
            //    if (walkDirection == WalkableDirection.Right)
            //    {
            //        walkDirection = WalkableDirection.Left;
            //    }
            //    else if (walkDirection == WalkableDirection.Left)
            //    {
            //        walkDirection = WalkableDirection.Right;
            //    }
            //    else
            //    {
            //        Debug.LogError("Current walkable direction is not set to legal values of right or left");
            //    }
            //}

            //public bool canMove
            //{
            //    get
            //    {
            //        return animator.GetBool(AnimationStrings.canMove);
            //    }
            //}

            //void Start()
            //{

            //}
            public void OnHit(int damage, Vector2 knockback)
    {

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
