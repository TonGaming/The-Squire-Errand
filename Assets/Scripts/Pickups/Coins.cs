using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] int coinValue = 10;

    bool isCollected;


    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")  && !isCollected)
        {
            // adding points to the scoring sys if only touched by the player Capsule
            FindAnyObjectByType<GameSession>().AddToScore(coinValue);

            isCollected = true;
        }
    }

   
}
