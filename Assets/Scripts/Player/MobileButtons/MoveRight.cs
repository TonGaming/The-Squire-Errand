using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerMovement playerMovement;

    Vector2 moveRightValue = new Vector2(1, 0);
    Vector2 notMovingValue = new Vector2(0, 0);


    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        playerMovement.OnMove(moveRightValue);


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Set biến bool là false khi người chơi nhả nút tấn công

        playerMovement.OnMove(notMovingValue);

    }
}
