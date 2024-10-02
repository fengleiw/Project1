using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    //public Vector2 moveSpeed = new Vector2(-0.5f, 0);
    public int damage = 40;
    Rigidbody2D rb;
    public Vector2 knockback = new Vector2(0, 0);
    public float speed = 5f;
    public float curveHeight = 2f;
    private float startTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }
    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //gameobject[] bomb = gameobject.findgameobjectswithtag("bomber");
        //gameobject player = gameobject.findwithtag("player");
            if (IsFacingRight())
            {
                float t = (Time.time - startTime);
                float x = t;
                float y = Mathf.Cos(t) * curveHeight;
                rb.velocity = new Vector2(-x * 0.5f, y);
            }
            else
            {
                float t = (Time.time - startTime);
            float x = t;
                float y = Mathf.Cos(t) * curveHeight;
                rb.velocity = new Vector2(x * 0.5f, y);
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            //Hit the target
            bool gotHit = damageable.Hit(damage, knockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
