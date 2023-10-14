using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField] int gemValue = 50;




    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            // cộng 1 vào persitent data player coin trong game session
            FindObjectOfType<GameSession>().AddToScore(gemValue);

            
        }
    }

   
}
