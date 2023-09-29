
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Get player rigidbody
    Rigidbody2D playerRigidbody;
    // Get Gun and Bullet
    [SerializeField] GameObject bullet;
    [SerializeField] Transform myGun;

    Animator playerAnimator;

    // check di chuyển
    bool isMoving;

    AudioSource playerAudioSource;
    [SerializeField] AudioClip arrowDrawingSound;
    [SerializeField] AudioClip arrowHitSound;
    [SerializeField] float volume = 0.5f;
    [SerializeField] float delayTime = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerAudioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movingCheck();
        PullBow();

    }

    void movingCheck()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) >= Mathf.Epsilon && Mathf.Abs(playerRigidbody.velocity.y) >= Mathf.Epsilon)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

    }

    void PullBow()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isMoving == false)
        {

            Invoke("ShootArrow", delayTime);
            playerAudioSource.PlayOneShot(arrowDrawingSound, volume);

            playerAnimator.SetBool("isDrawing", true);
        }
        else if (isMoving == true)
        {
            playerAnimator.SetBool("isDrawing", false);

        }
    }

    void ShootArrow()
    {
        Instantiate(bullet, myGun.position, transform.rotation);
    }

}

