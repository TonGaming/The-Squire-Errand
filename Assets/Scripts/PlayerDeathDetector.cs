using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    [SerializeField] float waitTillReload = 1f;
    //[SerializeField] float volume = 1f;

    [SerializeField] AudioSource DeathSound;

    //float baseGravity = 2f;
    bool isAlive = true;

    // Khai báo biến cho player
    PlayerInput myPlayerInput;
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    Transform myTransform;
    CapsuleCollider2D myCapsuleCollider2D;
    CircleCollider2D myCircleCollider2D;


    // Khai báo Vector lực đẩy khi chết
    [SerializeField] Vector2 deadKickFromLeft = new Vector2(10f, 10f);
    [SerializeField] Vector2 deadKickFromRight = new Vector2(-10f, 10f);



    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();

    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Hazard") && isAlive)
        {
            myPlayerInput.enabled = false;
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isRunning", false);
            Invoke("reloadScene", waitTillReload);

            // chỉnh lại trọng lực
            //myRigidbody2D.gravityScale = baseGravity;

            // chạy âm thanh khi chết 
            DeathSound.Play();

            // chết r thì vô chạm và rơi khỏi trò chơi luôn - nếu thích giữ lại ng lại khi chết thì cmt 2 dòng này lại
            myCapsuleCollider2D.isTrigger = true;
            myCircleCollider2D.isTrigger = true;

            // set isAlive 
            isAlive = false;

            // Chạy animation và deadKick tuỳ vào hướng va chạm
            // Nếu nhân vật đang quay phải
            if (myTransform.localScale.x >= 0)
            {
                myRigidbody2D.velocity = deadKickFromLeft;
                myRigidbody2D.gravityScale = 2f;
                
                Debug.Log("YOU ARE DEADDDDD");
                // trigger animation 
                myAnimator.SetTrigger("isDeadLeft");
            }
            // Nếu nhân vật đang quay trái
            else if (myTransform.localScale.x < 0)
            {
                myRigidbody2D.velocity = deadKickFromRight;

                // trigger animation
                myAnimator.SetTrigger("isDeadRight");
            }

            else { return; }
            
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
