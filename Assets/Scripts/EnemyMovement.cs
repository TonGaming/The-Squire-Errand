using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // enemy moving speed
    [SerializeField] float moveSpeed = 2f;

    Rigidbody2D myRigidbody2D;
    BoxCollider2D myBoxCollider2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        MovingLeftRight();

        Debug.Log(myRigidbody2D.velocity.x);

    }

    void MovingLeftRight()
    {
        myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
        // chạm tường thì đổi hướng di chuyển 
        moveSpeed = -moveSpeed;

        // chạm tường thì quay mặt lại 
        // Mathf.Sign để biến vận tốc từ 2, 3, 4,... bnh đi nữa -> 1 hoặc -1 r từ đó flip
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);

        Debug.Log("I FLIPPED");

        }
        else
        {
            return;
        }
    }




}
