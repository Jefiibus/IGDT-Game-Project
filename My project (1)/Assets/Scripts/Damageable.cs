using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class damageable : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public int _health = 3;
    void Start()
        {
            healthText.text = "Health: " + _health;
        }
    // returns Health
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private bool _isAlive = true;
    private bool isInvincible = false;
    private float timeSinceHit;
    public float invincibilityTime = 0.25f;

    // Way to tell if alive or not
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            Debug.Log("IsAlive set " + value);
        }
    }
    
    // Invincibility timer
    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    // For damage
    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            healthText.text = "Health: " + _health;
        }
    }
}