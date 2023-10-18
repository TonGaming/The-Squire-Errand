using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTwoSidesDeath : MonoBehaviour
{
    //[SerializeField] float LevelLoadDelay = 1f;

    //[SerializeField] float pushBackForce = 10f;

    CapsuleCollider2D enemyCapsuleCollider;
    Transform enemyTransform;
    Animator enemyAnimator;
    Rigidbody2D enemyRigidbody2D;



    //SpriteRenderer enemySpriteRenderer;
    EdgeCollider2D enemyEdgeCollider;

    // Start is called before the first frame update
    void Start()
    {
        //enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyTransform = GetComponent<Transform>();
        enemyAnimator = GetComponent<Animator>();
        enemyEdgeCollider = GetComponent<EdgeCollider2D>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // edge Collider là mặt lưng
        Collider2D enemyCollider = collision.otherCollider ;
        if (enemyCollider is EdgeCollider2D 
            && collision.gameObject.CompareTag("Bullet")
            && FindObjectOfType<EnemyDeathDetector>().GetIsAliveState() == true)
        {
            //Vector2 enemyFacing = new Vector2(-enemyTransform.localScale.x, 1f);

            // hướng ngã xuống chính là hướng ngược lại (do bị bắn từ sau lưng)
            //enemyTransform.localScale = enemyFacing;

            // hit turn the enemy aroud 
            //enemyRigidbody2D.velocity = new Vector2(enemyTransform.localScale.x * pushBackForce, 20f);

            Debug.Log("TOI BI BAN TU SAU LUNG");


        }
        // Capsule Collider là mặt tiền
        else if (enemyCollider is CapsuleCollider2D 
            && collision.gameObject.CompareTag("Bullet")
            && FindObjectOfType<EnemyDeathDetector>().GetIsAliveState() == true)
        {
            //Vector2 enemyFacing = new Vector2(enemyTransform.localScale.x, 1f);

            // hướng ngã xuống là hướng đang nhìn về (do bị bắn từ trước mặt)
            //enemyTransform.localScale = enemyFacing;

            //enemyRigidbody2D.velocity = new Vector2(-enemyTransform.localScale.x * pushBackForce, 20f);

            Debug.Log("TOI BI BAN TU TRUOC MAT");



        }
    }
    
}
