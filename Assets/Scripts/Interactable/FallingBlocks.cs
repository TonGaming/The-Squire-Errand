using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    BoxCollider2D blockBoxCollider;
    Rigidbody2D blockRigidBody;

    [SerializeField] float destroyDelay = 1f;
    [SerializeField] float gravityForce = 2f;


    bool isFalling = false;
    void Start()
    {
        blockBoxCollider = GetComponent<BoxCollider2D>();
        blockRigidBody = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isFalling == false)
        {
            isFalling = true;

            Invoke("FallingDown", destroyDelay - 0.4f);
            
            Invoke("KillBlocks", destroyDelay);
        }  
    }

    void FallingDown()
    {
        blockRigidBody.gravityScale = gravityForce;
        
    }

    void KillBlocks()
    {

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
