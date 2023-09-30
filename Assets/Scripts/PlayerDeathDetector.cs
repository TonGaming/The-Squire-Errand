using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    [SerializeField] float waitTillReload = 1f;
    float baseGravity = 2f;
    bool isAlive = true;

    // Khai báo biến PlayerInput
    PlayerInput myPlayerInput;
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    Transform myTransform;
    CapsuleCollider2D myCapsuleCollider2D;
    CircleCollider2D myCircleCollider2D;

    // Khai báo Vector lực đẩy khi chết
    [SerializeField] Vector2 deadKickFromLeft = new Vector2(10f, 10f);
    [SerializeField] Vector2 deadKickFromRight = new Vector2(-10f, 10f);

    // Âm thanh khi chết 
    AudioSource myAudioSource;
    [SerializeField] AudioClip DeathSound;

    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Hazard")) && isAlive)
        {
            myPlayerInput.enabled = false;
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isRunning", false);

            // Chạy animation và deadKick tuỳ vào hướng va chạm
            // Nếu nhân vật đang quay phải
            if (myTransform.localScale.x >= 0)
            {
                myRigidbody2D.velocity = deadKickFromLeft;

                Debug.Log("X velocity is: " + myRigidbody2D.velocity.x);

                // trigger animation 
                myAnimator.SetTrigger("isDeadLeft");

                // chết r thì vô chạm và rơi khỏi trò chơi luôn - nếu thích giữ lại ng lại khi chết thì cmt 2 dòng này lại
                myCapsuleCollider2D.isTrigger = true;
                myCircleCollider2D.isTrigger = true;

                myRigidbody2D.gravityScale = baseGravity;
                isAlive = false;
                myAudioSource.PlayOneShot(DeathSound,  1f);

                Invoke("reloadScene", waitTillReload);
            }
            // Nếu nhân vật đang quay trái
            else if (myTransform.localScale.x < 0)
            {

                Debug.Log("X velocity is: " + myRigidbody2D.velocity.x);

                myRigidbody2D.velocity = deadKickFromRight;

                myAnimator.SetTrigger("isDeadRight");

                // chết r thì vô chạm và rơi khỏi trò chơi luôn - nếu thích giữ lại ng lại khi chết thì cmt 2 dòng này lại
                myCapsuleCollider2D.isTrigger = true;
                myCircleCollider2D.isTrigger = true;

                myRigidbody2D.gravityScale = baseGravity;
                isAlive = false;
                myAudioSource.PlayOneShot(DeathSound, 1f);

                Invoke("reloadScene", waitTillReload);
            }

            else
            {
                return;
            }

        }
    }

    // Get Player Living Status 
    public bool GetIsAliveState()
    {
        return isAlive;
    }
    

    void reloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
