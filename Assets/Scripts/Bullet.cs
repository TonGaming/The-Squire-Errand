using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 15;

    Rigidbody2D bulletRigidbody;
    Transform bulletTransform;


    Vector2 bulletTrajectory;

    Vector2 bulletDirection;

    // get ra player 
    PlayerMovement myPlayer;



    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletTransform = GetComponent<Transform>();


        
        // Get ra player ở một script playermovement
        myPlayer = FindObjectOfType<PlayerMovement>();

        // Làm đường đạn
        bulletTrajectory = new (myPlayer.transform.localScale.x * bulletSpeed, 0f);

        // Làm hướng của viên đạn
        bulletDirection = myPlayer.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {

        BulletHeading();
    }


    void BulletHeading()
    {
        BulletFiring();
    }

    void BulletFiring()
    {
        bulletRigidbody.velocity = bulletTrajectory; // bắn đạn ra
        bulletTransform.localScale = bulletDirection; // đạn quay hướng nào
    }
}



