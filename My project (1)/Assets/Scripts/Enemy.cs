using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float chaseRange = 20f;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
        }
    }
}
