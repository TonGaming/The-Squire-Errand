using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickupSound;
    [SerializeField] float destroyDelay = .5f;
    [SerializeField] Vector2 coinVelocity = new Vector2(0f, 3f);

    Rigidbody2D coinRigidbody;
    CircleCollider2D coinCircleCollider;
    AudioSource coinAudioSource;

    void Start()
    {
        coinAudioSource = GetComponent<AudioSource>();
        coinCircleCollider = GetComponent<CircleCollider2D>();
        coinRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // cộng 1 vào persitent data player coin trong game session
            FindObjectOfType<GameSession>().ProcessPlayerCoin();

            // chạy audio
            coinAudioSource.PlayOneShot(CoinPickupSound);

            // tắt collider đi để ngăn âm thanh chạy nhiều lần
            coinCircleCollider.enabled = false;

            // tạo hiệu ứng bay lên khi chạm vào đồng xu
            coinRigidbody.velocity = coinVelocity;

            // sau 2s thì destroy gameObject
            Invoke("KillCoin", destroyDelay);
        }
    }

    void KillCoin()
    {
        Destroy(gameObject);
    }
}
