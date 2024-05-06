
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Get player components
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


    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        playerAudioSource = GetComponent<AudioSource>();
        playerAnimator = FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>();

        playerDeathDetector = FindAnyObjectByType<PlayerDeathDetector>();

    }

    // Update is called once per frame
    void Update()
    {

        PullBow();

        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // Set biến bool là true khi người chơi ấn nút tấn công
        isAttacking = true;

        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Set biến bool là false khi người chơi nhả nút tấn công
        isAttacking = false;

        
    }

    void PullBow()
    {
        if (isAttacking
            //&& !(FindAnyObjectByType<PlayerMovement>().GetIsMoving()) 
            && isPullingBow == false
            && (playerMovement.GetClimbableState() == false || playerMovement.GetGroundedState() == true))
        {
            Debug.Log("Is pulling bow");
            isPullingBow = true;

            Invoke("ShootArrow", delayTime); // bắn tên sau tầm .8s chạy animation
            Invoke("GoBackToIdling", delayTime + 0.2f); // sau khi mũi tên xuất hiện thì về lại chế độ bthg

            playerAudioSource.PlayOneShot(arrowDrawingSound, volume);

            FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isDrawing", true);

        }
        else if (!isAttacking 
            || FindAnyObjectByType<PlayerMovement>().GetIsMoving() == true 
            || playerDeathDetector.GetIsAliveState() == false )
        {
            Debug.Log("Is not pulling bow");

            isPullingBow = false;

            playerAudioSource.Stop();

            FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isDrawing", false);
        }
    }

    void ShootArrow()
    {
        if (isPullingBow && FindAnyObjectByType<PlayerMovement>().GetGroundedState() == true) 
        {
            Instantiate(bullet, myGun.position, transform.rotation); // spawn arrow
            playerAudioSource.PlayOneShot(arrowWhizzlingSound, volume); // chạy âm thanh xé gió
        }
        // nếu chạm thang nhưng k chạm sàn - meaning đang bám thang hoặc đang trèo thang thì huỷ luôn k bắn 
        else if (FindAnyObjectByType<PlayerMovement>().GetClimbableState() == true && playerMovement.GetGroundedState() == false )
        {
            return;
        }
        else { return; }
    }

    void GoBackToIdling()
    {
        isPullingBow = false;
        FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isIdling", true);
        FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isDrawing", false);
    }
}

