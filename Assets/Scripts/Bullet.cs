using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 15;
    Rigidbody2D myRigidbody;

    Transform bulletTransform;

    // get ra player 
    PlayerMovement myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // Get ra player 
        myPlayer = FindObjectOfType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        FlipBulletSprite();
    }
    void FlipBulletSprite()
    {
        if (myPlayer.transform.localScale.x >= 0)
        {
            Vector2 bulletDirection = new(1 * bulletSpeed, 1);
            myRigidbody.velocity = bulletDirection;
        }
        else if (myPlayer.transform.localScale.x < 0)
        {
            Vector2 bulletDirection = new(-1 * bulletSpeed, 1);
            myRigidbody.velocity = bulletDirection;
        }
    }
}
