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
    CapsuleCollider2D bulletCapsuleCollider;
    Transform bulletTransform;
    SpriteRenderer bulletSpriteRenderer;

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
        bulletCapsuleCollider = GetComponent<CapsuleCollider2D>();
        bulletSpriteRenderer = GetComponent<SpriteRenderer>();

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
        ArrowHeading();

    }

    void ArrowHeading()
    {
        bulletRigidbody.velocity = bulletTrajectory; // bắn đạn ra
        bulletTransform.localScale = bulletDirection; // đạn quay hướng nào
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Ground")) && isFlying == true)
        {
            // audio will run 
            bulletAudioSource.PlayOneShot(arrowImpact, volume);

            isFlying = false;

            bulletCapsuleCollider.isTrigger = true;

            // hide the arrow away for a while 
            Vector2 bulletHiding = new (-365f, -365f);
            bulletTransform.position = bulletHiding;

            // destroy the bullet after bullet's audio have been played
            Invoke("DestroyArrow", delayTime);

            
        }
        
    }
    


    

    void DestroyArrow()
    {
        Destroy(gameObject, 0f);
        isFlying = true;
    }
}