using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // enemy moving speed
    [SerializeField] float moveSpeed;

    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    CapsuleCollider2D enemyCapsuleCollider;
    bool isAlive;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        isAlive = true;
    }

    void Update()
    {

        EnemyMoving();




        Death();

    }


    //Làm enemy quay đầu = TriggerEnter2D và CompareTag -> phương án tối ưu
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            ReverseDirection();
            FlipSprite();
        }
        else
        {
            return;
        }
    }

    // tách hàm cho tường minh, dễ tái sử dụng
    // Làm enemy di chuyển 
    void EnemyMoving()
    {
        if (isAlive)
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0);

        }
        else if (!isAlive)
        {
            // khoá di chuyển khi chết
            enemyRigidbody.velocity = new Vector2(0f, 0f);

        }

    }

    // lật mặt
    void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
    }

    // chuyển hướng
    void ReverseDirection()
    {
        moveSpeed = -moveSpeed;
    }

    void Death()
    {
        // nếu ăn đạn và vẫn sống thì ...
        if (enemyRigidbody.IsTouchingLayers(LayerMask.GetMask("Bullet")) && isAlive)
        {
            // Destroy(gameObject, delayTime); // huỷ object

            Invoke("MakeCorpses", 0.1f);

            enemyAnimator.SetBool("isDead", true); // chạy animation chết one side(nếu có)
            isAlive = false;
        }
        else
        {
            return;
        }
    }

    void MakeCorpses()
    {
        Collider2D[] colliderLibrary = GetComponentsInChildren<Collider2D>();

        foreach(Collider2D childColliders in colliderLibrary)
        {
            childColliders.isTrigger = true;
        }

    }


}
