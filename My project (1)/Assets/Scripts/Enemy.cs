using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private float chaseRange = 10f;
    private Rigidbody2D enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player !=null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
            if (distanceToPlayer <= chaseRange)
            {
                Vector2 lookDirection = (player.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        damageable damageable = collision.gameObject.GetComponent<damageable>();
        if (collision.gameObject.CompareTag("Player") && damageable.Health != 0)
        {
            damageable.Hit(1);
            Debug.Log("Player hit");
        }
        if (collision.gameObject.CompareTag("Player") && damageable.Health == 0) 
        {
            damageable.IsAlive = false;
        }
    }
}
