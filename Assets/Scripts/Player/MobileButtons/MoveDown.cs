using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector2 MoveDownValue = new Vector2(0, -1);
    Vector2 NotMovingValue = new Vector2(0, 0);

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

        FindAnyObjectByType<PlayerMovement>().ClimbLadder(MoveDownValue);


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FindAnyObjectByType<PlayerMovement>().ClimbLadder(NotMovingValue);


    }
}
