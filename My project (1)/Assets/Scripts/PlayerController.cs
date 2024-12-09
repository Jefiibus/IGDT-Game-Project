using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string up = "w";
    public string down = "s";
    public string left = "a";
    public string right = "d";
    public float speed = 1.0f;
    
    public float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(up))
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(down))
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if(Input.GetKey(right))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

    }
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Player score: " + score);
    }
}
