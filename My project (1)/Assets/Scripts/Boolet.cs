using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolet : MonoBehaviour
{
    public float bulletSpeed = 50f;
    public float damage = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //goes forward at a set speed
        transform.Translate(Vector2.up * Time.deltaTime * bulletSpeed);

        //destroys itself once out of bounds
        if (Mathf.Abs(transform.position.x) > 500 || Mathf.Abs(transform.position.z) > 500)
        {
            Destroy(this.gameObject);
        }

        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        damageable damageable = collision.gameObject.GetComponent<damageable>();
        Enemy chaserange = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("Enemy") && damageable.Health != 0)
        {
            Destroy(this.gameObject);
            damageable.Hit(1);
            chaserange.chaseRange = 900;
            Debug.Log("Hit enemy");
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        { 
            Destroy(this.gameObject); 
        }
        if (collision.gameObject.CompareTag("Enemy") && damageable.Health == 0) 
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Enemy Destroyed");
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //if (collision.collider.CompareTag("Enemy"))
    //{
    //collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
    //Destroy(gameObject);
    //}
    //else
    //{
    //Debug.Log("not enemy");
    //}
    //}
}
