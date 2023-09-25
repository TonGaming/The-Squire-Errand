using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;
    [SerializeField] float deadVertical = 8f;
    [SerializeField] float deadHorizontal = 8f;

    // Khai báo biến PlayerInput
    PlayerInput myPlayerInput;
    Animator myAnimator;
    Rigidbody2D myRigidbody2D;
    Transform myTransform;

    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
            Invoke("reloadScene", delayTime);
            
        }
    }

    void Die()
    {
        myPlayerInput.enabled = false;
        myAnimator.SetBool("isJumping", false);
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetTrigger("isDead");
        if (myTransform.localScale.x >= 0)
        {

            Vector2 deadLeft = new Vector2(deadHorizontal, deadVertical);
            myRigidbody2D.velocity = deadLeft;
            myAnimator.SetTrigger("isDeadLeft");
            
        }
        else if (myTransform.localScale.x < 0)
        {
            
            
            myAnimator.SetTrigger("isDeadRight");
            
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
