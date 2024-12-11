using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float chaseRange = 10f;
    private Rigidbody2D enemyRb;
    private GameObject player;
    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerControllerScript = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
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
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg-90;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
          
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        damageable damageable = collision.gameObject.GetComponent<damageable>();
        if (collision.gameObject.CompareTag("Player") && damageable.Health != 0 && playerControllerScript.iFrames)
        {
            damageable.Hit(1);
            Debug.Log("Player hit");
        }
        if (collision.gameObject.CompareTag("Player") && damageable.Health == 0 && playerControllerScript.iFrames) 
        {
            damageable.IsAlive = false;
        }
    }
}
