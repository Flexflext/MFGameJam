﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealthStats stats;

    [SerializeField] private float health;
    private float maxHealth;

    [SerializeField] private float maxInvincibleTime;
    private float currentInvincibleTime;
    
    [SerializeField] private float maxStunTime;
    private float currentStunTime;

    private bool isInvincible;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        maxHealth = health;
        stats.MaxHealth = health;
        stats.CurrentHealth = health;
    }

    private void Update()
    {
        if (currentInvincibleTime > 0)
        {
            currentInvincibleTime -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }

        if (currentStunTime > 0)
        {
            currentStunTime -= Time.deltaTime;
        }
        else
        {
            controller.PlayerIsStunned = false;
        }
    }

    public void TakeDamage(float _dmg)
    {
        if (isInvincible)
        {
            return;
        }

        health -= _dmg;
        
        isInvincible = true;
        currentInvincibleTime = maxInvincibleTime;

        if (health <= 0)
        {
            Debug.Log("Im dead");
            stats.CurrentHealth = 0;
        }

        stats.CurrentHealth = health;

    }

    public void StunPlayer()
    {
        controller.PlayerIsStunned = true;
        currentStunTime = maxStunTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HpPickUp"))
        {
            HpPickUpStats pickUp = collision.GetComponent<HpPickUpStats>();

            if (health < maxHealth)
            {
                health += pickUp.Health;

                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }

            Destroy(collision.gameObject);
        }
    }
}