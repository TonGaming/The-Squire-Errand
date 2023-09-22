using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;


    // Khai báo biến PlayerInput
    PlayerInput myPlayerInput;
    Animator myAnimator;

    void Start()
    {
        myPlayerInput = GetComponent<PlayerInput>();
        myAnimator = GetComponent<Animator>();
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
    }

    void reloadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
