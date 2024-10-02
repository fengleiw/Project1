using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class FindPlayerProjectile : MonoBehaviour
{
    public int damage = 40;
    Rigidbody2D rb;
    public Vector2 knockback = new Vector2(0, 0);
    public float speed = 5f;
    public float curveHeight = 2f;

    private Transform player; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        FindPlayer(); 
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        if (player == null)
        {
            Debug.LogError("Cannot find player");
        }
    }

    void Update()
    {
        if (player != null)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float t = Time.time;
        float x = Mathf.Cos(t) * curveHeight;
        float y = Mathf.Sin(t) * curveHeight;

        rb.velocity = new Vector2(x, y) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            // Hit the target
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