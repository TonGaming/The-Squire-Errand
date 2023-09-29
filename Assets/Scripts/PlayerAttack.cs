using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Get Gun and Bullet
    [SerializeField] GameObject bullet;
    [SerializeField] Transform myGun;

    void OnFire(InputValue value)
    {
        Instantiate(bullet, myGun.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
