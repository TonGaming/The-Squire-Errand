
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Get player components
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;

    PlayerMovement playerMovement;
    PlayerDeathDetector playerDeathDetector;

    // Get Gun and Bullet
    [SerializeField] GameObject bullet;
    [SerializeField] Transform myGun;



    // check di chuyển và kéo cung
    bool isMoving;
    bool isPullingBow = false;

    AudioSource playerAudioSource;
    [SerializeField] AudioClip arrowDrawingSound;
    [SerializeField] AudioClip arrowWhizzlingSound;


    float volume = 0.5f;
    float delayTime = 1.2f;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerAudioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();

        playerDeathDetector = FindObjectOfType<PlayerDeathDetector>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCheck();
        PullBow();

        
    }

    void MoveCheck()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) >= 1 || Mathf.Abs(playerRigidbody.velocity.y) >= 1)
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
        if (Input.GetKey(KeyCode.Mouse0) 
            && isMoving == false 
            && isPullingBow == false
            && (playerMovement.GetClimbableState() == false || playerMovement.GetGroundedState() == true))
        {
            isPullingBow = true;

            Invoke("ShootArrow", delayTime); // bắn tên sau tầm .8s chạy animation
            Invoke("GoBackToIdling", delayTime + 0.2f); // sau khi mũi tên xuất hiện thì về lại chế độ bthg

            playerAudioSource.PlayOneShot(arrowDrawingSound, volume);

            playerAnimator.SetBool("isDrawing", true);

        }
        else if (isMoving == true || playerDeathDetector.GetIsAliveState() == false )
        {
            isPullingBow = false;

            playerAudioSource.Stop();

            playerAnimator.SetBool("isDrawing", false);
        }
    }

    void ShootArrow()
    {
        if (isPullingBow && playerMovement.GetGroundedState() == true) 
        {
            Instantiate(bullet, myGun.position, transform.rotation); // spawn arrow
            playerAudioSource.PlayOneShot(arrowWhizzlingSound, volume); // chạy âm thanh xé gió
        }
        // nếu chạm thang nhưng k chạm sàn - meaning đang bám thang hoặc đang trèo thang thì huỷ luôn k bắn 
        else if (playerMovement.GetClimbableState() == true && playerMovement.GetGroundedState() == false )
        {
            return;
        }
        else { return; }
    }

    void GoBackToIdling()
    {
        isPullingBow = false;
        playerAnimator.SetBool("isIdling", true);
        playerAnimator.SetBool("isDrawing", false);
    }
}

