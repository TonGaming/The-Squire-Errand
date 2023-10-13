using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    Rigidbody2D heartsRigidbody;
    CircleCollider2D heartsCircleCollider;

    [SerializeField] AudioClip heartsPickupSound;
    bool isHealing = false;

    [SerializeField] Vector2 heartsVelocity = new Vector2(0f, 3f);

    GameSession gameSession;

    [SerializeField] float killDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        heartsCircleCollider = GetComponent<CircleCollider2D>();
        heartsRigidbody = GetComponent<Rigidbody2D>();


        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isHealing == false)
        {
            gameSession.AddHealth();

            heartsRigidbody.velocity = heartsVelocity;

            AudioSource.PlayClipAtPoint(heartsPickupSound,Camera.main.transform.position);

            StartCoroutine(KillHearts());

            isHealing = true;
            Invoke("SetIsHealing", .2f);

            heartsCircleCollider.enabled = false;
        }
        else
        {
            return;
        }
    }

    void SetIsHealing()
    {
        isHealing = false;
    }

    IEnumerator KillHearts()
    {
        yield return new WaitForSecondsRealtime(killDelay);

        Destroy(gameObject);
    }
}
