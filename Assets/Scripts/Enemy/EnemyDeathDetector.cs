using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathDetector : MonoBehaviour
{
    // enemy moving speed

    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;




    [SerializeField] int enemyHealth = 4;

    bool isAlive = true;



    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();


    }

    void Update()
    {
        
        GetIsAliveState();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && enemyHealth > 0)
        {
            Debug.Log("Ke dich da an dame nhung k chet");

            enemyAnimator.SetBool("isHurt", true);

            enemyHealth--;



            Invoke("ResetIsHurtState", 0.4f);
        } 
        else if (collision.gameObject.CompareTag("Bullet") && enemyHealth == 0)
        {
            enemyAnimator.SetBool("isDead", true);

            Invoke("MakeCorpses", 0.1f);

            isAlive = false;
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

    public bool GetIsAliveState()
    {
        return isAlive;
    }

    void ResetIsHurtState()
    {
        enemyAnimator.SetBool("isHurt", false);
    }

}
