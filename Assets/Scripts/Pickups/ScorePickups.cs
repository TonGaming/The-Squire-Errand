using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickups : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSound;
    [SerializeField] float volume = 0.5f;

    bool isCollected = false;

    [SerializeField] float destroyDelay = .5f;
    [SerializeField] Vector2 coinVelocity = new Vector2(0f, 3f);

    GameSession gameSession;

    Rigidbody2D coinRigidbody;
    CircleCollider2D coinCircleCollider;
    AudioSource coinAudioSource;

    void Start()
    {
        coinAudioSource = GetComponent<AudioSource>();
        coinCircleCollider = GetComponent<CircleCollider2D>();
        coinRigidbody = GetComponent<Rigidbody2D>();
        gameSession = FindObjectOfType<GameSession>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isCollected == false)
        {
            gameSession.ProcessPlayerScore();

            isCollected = true;

            // chạy audio
            coinAudioSource.PlayOneShot(coinPickupSound, volume);
            // PlayClipAtPoint sẽ không bị mất đi khi huỷ gameObject 

            // tắt đi để ngăn kích hoạt pickups nhiều lần
            gameObject.SetActive(false);

            // tạo hiệu ứng bay lên khi chạm vào đồng xu
            coinRigidbody.velocity = coinVelocity;

            // sau 2s thì destroy gameObject
            Invoke("KillObject", destroyDelay);
        }
    }

    void KillObject()
    {
        Destroy(gameObject);
    }
}
