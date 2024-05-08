using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector2 MoveDownValue = new Vector2(0,-1);
    Vector2 NotMovingValue = new Vector2(0,0);

    public void OnPointerDown(PointerEventData eventData)
    {

        FindAnyObjectByType<PlayerMovement>().OnMove(MoveDownValue);


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FindAnyObjectByType<PlayerMovement>().OnMove(NotMovingValue);


    }
}
