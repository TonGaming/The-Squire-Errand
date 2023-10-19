using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // enemy moving speed
    [SerializeField] float moveSpeed;
    [SerializeField] float stunTime = 1f;

    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;

    EnemyDeathDetector enemyDeathDetector;


    bool isShot = false;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();

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
        if (enemyCollider is EdgeCollider2D && collision.gameObject.CompareTag("Bullet") && enemyDeathDetector.GetEnemyHealth() > 1)
        {
            isShot = true;

            // Phải làm animation đứng im bị thương vào đây
            //enemyAnimator.SetBool
            

            StartCoroutine(HealEnemyAfterShot());



            FlipSprite();


            Debug.Log("Toi bi ban vao lung roiii");



        }
        else if (enemyCollider is EdgeCollider2D && collision.gameObject.CompareTag("Bullet") && enemyDeathDetector.GetEnemyHealth() <= 1)
        {
            FlipSprite();



            Debug.Log("Toi da chet trong movement");


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
        else if (enemyDeathDetector.GetIsAliveState() == false)
        {
            enemyRigidbody.velocity = new Vector2(0, 0);
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

    void ResetIsShotState()
    {
        isShot = false;
    }


}
