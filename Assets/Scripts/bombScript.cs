using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    public DetectionZone attackZone;
    Animator animator;
    Damageable damageable;
    public DetectionZone detectionZone;
    public DetectionZone cliffDetectionZone;
    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set { _hasTarget = value; animator.SetBool(AnimationStrings.hasTarget, value); } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
