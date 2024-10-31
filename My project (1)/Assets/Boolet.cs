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
        transform.Translate(Vector2.left * Time.deltaTime * bulletSpeed);

        //destroys itself once out of bounds
        if (Mathf.Abs(transform.position.x) > 500 || Mathf.Abs(transform.position.z) > 500)
        {
            Destroy(this.gameObject);
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
