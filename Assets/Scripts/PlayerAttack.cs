
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

    // check di chuyển và kéo cung
    bool isMoving;
    bool isPullingBow = false;

    AudioSource playerAudioSource;
    [SerializeField] AudioClip arrowDrawingSound;
    [SerializeField] AudioClip arrowWhizzlingSound;

    [SerializeField] float volume = 0.7f;
    float delayTime = 1.2f;


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
        MoveCheck();
        PullBow();

    }

    void MoveCheck()
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
        if (Input.GetKey(KeyCode.Mouse0) && isMoving == false && isPullingBow == false)
        {
            isPullingBow = true;

            Invoke("ShootArrow", delayTime); // bắn tên sau tầm .8s chạy animation
            Invoke("GoBackToIdling", delayTime + 0.2f); // sau khi mũi tên xuất hiện thì về lại chế độ bthg

            playerAudioSource.PlayOneShot(arrowDrawingSound, volume);

            playerAnimator.SetBool("isDrawing", true);

        }
        else if (isMoving == true)
        {
            isPullingBow = false;

            playerAudioSource.Stop();

            playerAnimator.SetBool("isDrawing", false);
        }
    }

    void ShootArrow()
    {
        if (isPullingBow)
        {
            Instantiate(bullet, myGun.position, transform.rotation); // spawn arrow
            playerAudioSource.PlayOneShot(arrowWhizzlingSound, volume); // chạy âm thanh xé gió
        }
        else
        {
            return;
        }
    }

    void GoBackToIdling()
    {
        isPullingBow = false;
        playerAnimator.SetBool("isIdling", true);
        playerAnimator.SetBool("isDrawing", false);
    }
}

