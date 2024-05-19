
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Get player components


    PlayerMovement playerMovement;
    PlayerDeathDetector playerDeathDetector;

    // Get Gun and Bullet
    [SerializeField] GameObject bullet;
    [SerializeField] Transform myGun;



    // check di chuyển và kéo cung
    bool isMoving;
    public bool isPullingBow = false;

    AudioSource audioSource;
    [SerializeField] AudioClip arrowDrawingSound;
    [SerializeField] AudioClip arrowWhizzlingSound;


    float volume = 0.5f;
    float delayTime = 1.2f;


    public bool isAttacking;

    void Start()
    {
        isAttacking = false;
        audioSource = GetComponent<AudioSource>();




    }




    public void PullBow()
    {
        Debug.Log("Hhihihihihi PULL BOW ACTIVATED");


        if (!(FindAnyObjectByType<PlayerMovement>().GetIsMoving())
            && isPullingBow == false
            && (FindAnyObjectByType<PlayerMovement>().GetClimbableState() == false || FindAnyObjectByType<PlayerMovement>().GetGroundedState() == true))
        {
            Debug.Log("Is pulling bow");
            isPullingBow = true;

            StartCoroutine(nameof(ShootArrow), delayTime); // bắn tên sau tầm .8s chạy animation
            StartCoroutine(nameof(GoBackToIdling), delayTime + 0.2f); // sau khi mũi tên xuất hiện thì về lại chế độ bthg

            audioSource.PlayOneShot(arrowDrawingSound, volume);

            FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isDrawing", true);

        }
        else if (
            FindAnyObjectByType<PlayerMovement>().GetIsMoving() == true
            || FindAnyObjectByType<PlayerDeathDetector>().GetIsAliveState() == false)
        {
            Debug.Log("Is not pulling bow");

            isPullingBow = false;

            audioSource.Stop();

            FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isDrawing", false);
        }
    }

    IEnumerator ShootArrow(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isPullingBow && FindAnyObjectByType<PlayerMovement>().GetGroundedState() == true)
        {
            Instantiate(bullet, myGun.position, transform.rotation); // spawn arrow
            audioSource.PlayOneShot(arrowWhizzlingSound, volume); // chạy âm thanh xé gió
        }


    }

    IEnumerator GoBackToIdling(float delay)
    {
        yield return new WaitForSeconds(delay);

        isPullingBow = false;
        FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isIdling", true);
        FindAnyObjectByType<PlayerMovement>().GetComponent<Animator>().SetBool("isDrawing", false);
    }
}

