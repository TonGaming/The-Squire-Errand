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


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {
        EnemyMoving();

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
        if (FindObjectOfType<EnemyDeathDetector>().GetIsAliveState() == true)
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0);

        }
        else if (FindObjectOfType<EnemyDeathDetector>().GetIsAliveState() == false)
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

    


}
