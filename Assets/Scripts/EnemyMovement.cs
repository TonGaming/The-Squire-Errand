using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // enemy moving speed
    [SerializeField] float moveSpeed ;

    Rigidbody2D myRigidbody2D;


    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        EnemyMoving();
        Debug.Log("X Velocity is: " + myRigidbody2D.velocity.x);
    
    }


    //Làm enemy quay đầu = TriggerEnter2D và CompareTag -> phương án tối ưu
    void OnTriggerEnter2D(Collider2D other)
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
        myRigidbody2D.velocity = new Vector2(moveSpeed, 0);
    }

    // lật mặt
    void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    }

    // chuyển hướng
    void ReverseDirection()
    {
        moveSpeed = -moveSpeed;
    }

    // How Lecturers do

    //void MovingLeftRight()
    //{
    //    myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    //}

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Ground"))
    //    {
    //    // chạm tường thì đổi hướng di chuyển 
    //    moveSpeed = -moveSpeed;

    //    // chạm tường thì quay mặt lại 
    //    // Mathf.Sign để biến vận tốc từ 2, 3, 4,... bnh đi nữa -> 1 hoặc -1 r từ đó flip
    //    transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);

    //    }
    //    else
    //    {
    //        return;
    //    }
    //}




}
