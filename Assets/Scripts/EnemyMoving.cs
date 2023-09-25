using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D myRigidbody2D;

    BoxCollider2D myBoxCollider2D;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        EnemyWalking();

    }

    void EnemyWalking()
    {
        myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    // Làm Quay đầu = LayerMask và GetMask giống như Player 
    //void WallCheck()
    //{
    //    if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
    //    {
    //        moveSpeed = -moveSpeed;
    //        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}
    // Sử dụng phương án này sẽ gây ra hiện tượng quay vòng vòng tại chỗ do Collider chạm vào Layer liên tục khi quay -> bỏ

    //Làm enemy quay đầu = TriggerEnter2D và CompareTag -> phương án tối ưu

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            FlipSprite();
            ReverseDirection();
        }
        else
        {
            return;
        }
    }

    // tách hàm cho tường minh, dễ tái sử dụng
    void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    }

    // tách hàm cho tường minh, dễ tái sử dụng
    void ReverseDirection()
    {
        moveSpeed = -moveSpeed;
    }
}
