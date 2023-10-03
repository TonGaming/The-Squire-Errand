using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTwoSidesDeath : MonoBehaviour
{
    //[SerializeField] float delayTime = 1f;

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
        FlipSpriteOnDamage();
    }

    void FlipSpriteOnDamage()
    {
        if (enemyEdgeCollider.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {
            Vector2 enemyFacing = new Vector2(-enemyTransform.localScale.x, 1f);

            // hướng ngã xuống chính là hướng ngược lại (do bị bắn từ sau lưng)
            enemyTransform.localScale = enemyFacing;
            
            
        }
        else if (enemyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {
            Vector2 enemyFacing = new Vector2(enemyTransform.localScale.x, 1f);

            // hướng ngã xuống là hướng đang nhìn về (do bị bắn từ trước mặt)
            enemyTransform.localScale = enemyFacing;

            


        }
    }
}
