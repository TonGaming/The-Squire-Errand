using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathDetector : MonoBehaviour
{
    // enemy moving speed


    Animator enemyAnimator;


    bool isAlive = true;

    [SerializeField] int enemyHealth = 4;





    void Start()
    {

        enemyAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        GetEnemyHealth();
        GetIsAliveState();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && enemyHealth > 1)
        {

            enemyAnimator.SetBool("isHurt", true);

            enemyHealth--;

            Invoke("ResetIsHurtState", 0.5f);
        }
        else if (collision.gameObject.CompareTag("Bullet") && enemyHealth <= 1)
        {
            enemyAnimator.SetBool("isDead", true);

            isAlive = false;

            MakeCorpses();

        }
    }

    void MakeCorpses()
    {
        Collider2D[] colliderLibrary = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D childColliders in colliderLibrary)
        {
            childColliders.enabled = false;
        }


    }

    void ResetIsHurtState()
    {
        enemyAnimator.SetBool("isHurt", false);
    }

    public int GetEnemyHealth()
    {
        return enemyHealth;
    }

    public bool GetIsAliveState()
    {
        return isAlive;
    }
}
