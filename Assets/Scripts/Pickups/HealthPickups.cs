using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] AudioClip heartsPickupSound;

    [SerializeField] Vector2 heartsVelocity = new Vector2(0f, 3f);
    
    Rigidbody2D heartsRigidbody;
    CircleCollider2D heartsCircleCollider;
    AudioSource heartsAudioSource;

    GameSession gameSession;



    [SerializeField] float volume = 0.6f;
    [SerializeField] float killDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        heartsCircleCollider = GetComponent<CircleCollider2D>();
        heartsRigidbody = GetComponent<Rigidbody2D>();
        heartsAudioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            gameSession.AddHealth();

            heartsRigidbody.velocity = heartsVelocity;

            heartsAudioSource.PlayOneShot(heartsPickupSound,volume);



            // tắt đi để ngăn kích hoạt pickups nhiều lần
            
            heartsCircleCollider.enabled = false;
            StartCoroutine(KillHearts());
        }
        else
        {
            return;
        }
    }


    IEnumerator KillHearts()
    {
        yield return new WaitForSecondsRealtime(killDelay);

        Destroy(gameObject);
    }
}
