using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // enemy moving speed
    [SerializeField] float moveSpeed;
    float stunTime = .6f;

    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    Transform enemyTransform;
    
    EnemyDeathDetector enemyDeathDetector;


    bool isShot = false;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyTransform = GetComponent<Transform>();
        enemyAnimator = GetComponent<Animator>();

        enemyDeathDetector = GetComponent<EnemyDeathDetector>();

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

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D enemyCollider = collision.otherCollider;
        if (enemyCollider is EdgeCollider2D && collision.gameObject.CompareTag("Bullet") && isShot == false)
        {
            isShot = true;

            FlipSprite();

            StartCoroutine(HealEnemyAfterShot());
            Debug.Log("Toi an dan vao lung r");

        }
        else if (enemyCollider is CapsuleCollider2D && collision.gameObject.CompareTag("Bullet") && isShot == false)
        {
            isShot = true;

            Invoke("ResetIsShotState", stunTime);
            Debug.Log("Toi an dan vao mat r ");
        }


    }

    IEnumerator HealEnemyAfterShot()
    {
        yield return new WaitForSecondsRealtime(stunTime);

        ResetIsShotState();

        ReverseDirection();

    }

    // tách hàm cho tường minh, dễ tái sử dụng
    // Làm enemy di chuyển 
    void EnemyMoving()
    {
        if (isShot == false && enemyDeathDetector.GetIsAliveState() == true)
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0);

        }
        else if (isShot == true && enemyDeathDetector.GetIsAliveState() == true)
        {
            enemyRigidbody.velocity = new Vector2(0, 0);

        }
        else if (enemyDeathDetector.GetIsAliveState() == false || enemyAnimator.GetBool("isHurt") == true)
        {
            enemyRigidbody.velocity = new Vector2(0, 0);
        }

    }

    // lật mặt
    public void FlipSprite()
    {
        enemyTransform.localScale = new Vector2(-(enemyTransform.localScale.x), enemyTransform.localScale.y);
    }

    // chuyển hướng
    void ReverseDirection()
    {
        moveSpeed = -moveSpeed;
    }

    void ResetIsShotState()
    {
        isShot = false;
    }


}
