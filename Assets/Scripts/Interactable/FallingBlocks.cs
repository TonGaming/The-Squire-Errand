using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    BoxCollider2D blockBoxCollider;
    Rigidbody2D blockRigidBody;

    [SerializeField] float destroyDelay = 2f;
    [SerializeField] Vector2 blockVelocity = new Vector2(0f, 10f);

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
            blockRigidBody.velocity = blockVelocity;
            Invoke("KillBlocks", destroyDelay);
        }  
    }

    void KillBlocks()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
