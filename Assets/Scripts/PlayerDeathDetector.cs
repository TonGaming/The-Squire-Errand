using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    [SerializeField] float waitTillReload = 2f;

    bool isAlive = true;

    // Khai báo biến PlayerInput
    PlayerInput myPlayerInput;
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    Transform myTransform;

    // Khai báo Vector lực đẩy khi chết
    [SerializeField] Vector2 deadKickFromLeft = new Vector2(10f, 10f);
    [SerializeField] Vector2 deadKickFromRight = new Vector2(-10f, 10f);

    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    private void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
            Invoke("reloadScene", waitTillReload);

        }
    }

    void Die()
    {
        myPlayerInput.enabled = false;
        myAnimator.SetBool("isJumping", false);
        myAnimator.SetBool("isRunning", false);

        // Chạy animation và deadKick tuỳ vào hướng va chạm
        // Nếu nhân vật đang quay phải
        if (myTransform.localScale.x >= 0 && isAlive)
        {
            myRigidbody2D.velocity = deadKickFromLeft;

            Debug.Log("X velocity is: " + myRigidbody2D.velocity.x);

            myAnimator.SetTrigger("isDeadLeft");
            isAlive = false;
        }
        // Nếu nhân vật đang quay trái
        else if (myTransform.localScale.x < 0 && isAlive)
        {
            Debug.Log("X velocity is: " + myRigidbody2D.velocity.x);

            myRigidbody2D.velocity = deadKickFromRight;

            myAnimator.SetTrigger("isDeadRight");
            isAlive = false;
        }

        else
        {
            return;
        }


    }

    void reloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
