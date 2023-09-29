using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    [SerializeField] float bulletSpeed = 15;
    Rigidbody2D bulletRigidbody;
    Vector2 bulletTrajectory;
>>>>>>> Stashed changes

    [SerializeField] float bulletSpeed = 15;

    Rigidbody2D bulletRigidbody;
    Transform bulletTransform;
<<<<<<< Updated upstream

    float bulletTrajectory;
    float bulletDirection;

=======
    Vector2 bulletDirection;
>>>>>>> Stashed changes
    // get ra player 
    PlayerMovement myPlayer;



    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletTransform = GetComponent<Transform>();


        
        // Get ra player ở một script playermovement
        myPlayer = FindObjectOfType<PlayerMovement>();

<<<<<<< Updated upstream
        // Hướng đạn bay dựa theo ng chơi
        bulletTrajectory = myPlayer.transform.localScale.x * bulletSpeed;

        // Mặt Sprite viên đạn dựa theo ng chơi
        bulletDirection = myPlayer.transform.localScale.x;
=======

        // Làm đường đạn
        bulletTrajectory = new (myPlayer.transform.localScale.x * bulletSpeed, 0f);
        // Làm hướng của viên đạn
        bulletDirection = myPlayer.transform.localScale;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        BulletHeading();
    }


    void BulletHeading()
    {
        // Hướng bay viên đạn
        bulletRigidbody.velocity = new (bulletTrajectory, 0f);

        // Mặt Sprite viên đạn
        bulletTransform.localScale = new Vector3(bulletDirection, 1, 1);
=======
        BulletFiring();
    }
    void BulletFiring()
    {
        bulletRigidbody.velocity = bulletTrajectory; // bắn đạn ra
        bulletTransform.localScale = bulletDirection; // đạn quay hướng nào
>>>>>>> Stashed changes
    }
}



