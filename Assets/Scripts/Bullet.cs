using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 15f;
    float delayTime = 1.2f;
    Rigidbody2D bulletRigidbody;
    Transform bulletTransform;

    AudioSource bulletAudioSource;
    [SerializeField] AudioClip arrowImpact;
    [SerializeField] float volume = .5f;

    Vector2 bulletTrajectory;
    Vector2 bulletDirection;

    // get ra player 
    PlayerMovement myPlayer;

    bool isFlying = true;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletTransform = GetComponent<Transform>();
        bulletAudioSource = GetComponent<AudioSource>();


        // Get ra player ở một script playermovement
        myPlayer = FindObjectOfType<PlayerMovement>();

        // Làm đường đạn
        bulletTrajectory = new(myPlayer.transform.localScale.x * bulletSpeed, 0f);

        // Làm hướng của viên đạn
        bulletDirection = myPlayer.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        BulletHeading();
        OnArrowImpact();
    }


    void BulletHeading()
    {
        ArrowFiring();
    }

    void ArrowFiring()
    {
        bulletRigidbody.velocity = bulletTrajectory; // bắn đạn ra
        bulletTransform.localScale = bulletDirection; // đạn quay hướng nào
    }

    void OnArrowImpact()
    {
        if (bulletRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground", "Enemy")) && isFlying == true)
        {

            bulletAudioSource.PlayOneShot(arrowImpact, volume);

            isFlying = false;

            Invoke("DestroyArrow", delayTime);


        }


    }

    void DestroyArrow()
    {
        Destroy(gameObject, 0f);
        isFlying = true;
    }
}