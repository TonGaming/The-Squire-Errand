using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathDetector : MonoBehaviour
{
    [SerializeField] float delayTime = 2f;

    CapsuleCollider2D myCapsuleCollider2D;

    void Start()
    {
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy" )))
        {
        Invoke("reloadScene", delayTime);
        }
    }

    void reloadScene()
    {
        SceneManager.LoadScene("Main Game Level");
    }
}
