using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnJump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerMovement playerMovement;

    


    // Start is called before the first frame update
    void Awake()
    {
        //playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        FindAnyObjectByType<PlayerMovement>().OnJump(1);


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Set biến bool là false khi người chơi nhả nút tấn công

        FindAnyObjectByType<PlayerMovement>().OnJump(-1);

    }
}
