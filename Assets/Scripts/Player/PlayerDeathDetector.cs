using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    //[SerializeField] float waitTillReload = 1f;
    //[SerializeField] float volume = 1f;

    [SerializeField] AudioSource DeathSound;

    float baseGravity = 2f;
    bool isAlive = true;

    // Khai báo biến cho player
    PlayerInput myPlayerInput;
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    Transform playerTransform;
    CapsuleCollider2D myCapsuleCollider2D;
    CircleCollider2D myCircleCollider2D;
    EdgeCollider2D myEdgeCollider2D;

    // Khai báo Game Session 
    GameSession playerGameSession;

    // Khai báo lực đẩy khi chết
    [SerializeField] float deadKickForce = 10f;
    //[SerializeField] Vector2 deadKickToUp = new Vector2(0f, 10f);


    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        myEdgeCollider2D = GetComponent<EdgeCollider2D>();

        playerGameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        CollidersOffOnDead();
    }




    void OnCollisionEnter2D(Collision2D other)
    {
        // Khai báo biến để so sánh các loại collider
        Collider2D playerCollider = other.otherCollider;


        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Hazard") && isAlive)
        {
            myPlayerInput.enabled = false;
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isRunning", false);


            // Gọi tới hàm GameSession để trừ PLayerLives đi 
            playerGameSession.ProcessPlayerDeath();

            // chỉnh lại trọng lực
            myRigidbody2D.gravityScale = baseGravity;

            // chạy âm thanh khi chết 
            DeathSound.Play();
            
            // set isAlive 
            isAlive = false;

            // Chạy animation và deadKick tuỳ vào hướng va chạm(collider đc chạm)
            // Xử lý va chạm của CapsuleCollider2D của player với enemy
            // Nếu chạm vào mặt và đang quay sang phải -> bay sang trái
            if (playerCollider is CapsuleCollider2D && playerTransform.localScale.x > Mathf.Epsilon)
            {
                // Vector deadkick
                myRigidbody2D.velocity = new Vector2(-playerTransform.localScale.x * deadKickForce, deadKickForce);

                // trigger animation 
                myAnimator.SetTrigger("isDeadBack");


            }
            // Nếu chạm mặt và đang quay sang trái -> bay sang phải
            else if (playerCollider is CapsuleCollider2D && playerTransform.localScale.x < 0)
            {
                // Vector deadkick
                myRigidbody2D.velocity = new Vector2(-playerTransform.localScale.x * deadKickForce, deadKickForce);

                // trigger animation 
                myAnimator.SetTrigger("isDeadBack");
            }
            // Xử lý va chạm của EdgeCollider2D(lưng) của player với enemy
            // Nếu chạm lưng và đang quay sang trái -> bay sang trái
            else if (playerCollider is EdgeCollider2D && playerTransform.localScale.x < 0)
            {
                myRigidbody2D.velocity = new Vector2(playerTransform.localScale.x * deadKickForce, deadKickForce);

                // trigger animation
                myAnimator.SetTrigger("isDeadFront");

            }
            // Nếu chạm lưng và đang quay sang phải -> bay sang phải
            else if (playerCollider is EdgeCollider2D && playerTransform.localScale.x > Mathf.Epsilon)
            {
                myRigidbody2D.velocity = new Vector2(playerTransform.localScale.x * deadKickForce, deadKickForce);

                // trigger animation
                myAnimator.SetTrigger("isDeadFront");
            }
            else if (playerCollider is CircleCollider2D)
            {
                myRigidbody2D.velocity = new Vector2(playerTransform.localScale.x * 0, deadKickForce + 5f);

                // trigger animation
                myAnimator.SetTrigger("isDeadBack");
            }

            else { return; }




        }

    }

    void CollidersOffOnDead()
    {
        if (isAlive == false)
        {
            Collider2D[] collidersLibrary = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D childCollider in collidersLibrary)
            {
                childCollider.isTrigger = true;
            }
        }
    }



    // Get Player Living Status 
    public bool GetIsAliveState()
    {
        return isAlive;
    }


    
}
