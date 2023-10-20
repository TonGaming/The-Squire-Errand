using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathDetector : MonoBehaviour
{
    // enemy moving speed
    EnemyMovement enemyMovement;

    Animator enemyAnimator;
    Transform enemyTransform;
    Rigidbody2D enemyRigidbody;

    bool isAlive = true;

    [SerializeField] int enemyHealth = 4;





    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimator = GetComponent<Animator>();
        enemyTransform = GetComponent<Transform>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetEnemyHealth();
        GetIsAliveState();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet") && enemyHealth > Mathf.Epsilon )
        {

            enemyAnimator.SetBool("isHurt", true);

            enemyHealth--;

            Invoke("ResetIsHurtState", 0.5f);
        }
        
        if ( enemyHealth == 0)
        {
            enemyAnimator.SetBool("isDead", true);

            Invoke("ResetLocalScale", Mathf.Epsilon);

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

        enemyRigidbody.velocity = new Vector2(0, 0);

    }

    void ResetLocalScale()
    {
        enemyTransform.localScale = new Vector2(-(enemyTransform.localScale.x), 1f);
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
