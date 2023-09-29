using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 15;

    Rigidbody2D bulletRigidbody;
    Transform bulletTransform;

    float bulletTrajectory;
    float bulletDirection;

    // get ra player 
    PlayerMovement myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletTransform = GetComponent<Transform>();

        // Get ra player 
        myPlayer = FindObjectOfType<PlayerMovement>();

        // Hướng đạn bay dựa theo ng chơi
        bulletTrajectory = myPlayer.transform.localScale.x * bulletSpeed;

        // Mặt Sprite viên đạn dựa theo ng chơi
        bulletDirection = myPlayer.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        BulletHeading();
    }


    void BulletHeading()
    {
        // Hướng bay viên đạn
        bulletRigidbody.velocity = new (bulletTrajectory, 0f);

        // Mặt Sprite viên đạn
        bulletTransform.localScale = new Vector3(bulletDirection, 1, 1);
    }
}



