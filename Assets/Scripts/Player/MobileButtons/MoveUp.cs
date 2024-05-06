using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector2 MoveUpValue = new Vector2(0, 1);
    Vector2 NotMovingValue = new Vector2(0, 0);

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        FindAnyObjectByType<PlayerMovement>().ClimbLadder(NotMovingValue);

        

    }


    public void OnPointerDown(PointerEventData eventData)
    {

        FindAnyObjectByType<PlayerMovement>().ClimbLadder(MoveUpValue);


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FindAnyObjectByType<PlayerMovement>().ClimbLadder(NotMovingValue);


    }
}
