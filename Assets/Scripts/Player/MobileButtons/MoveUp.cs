using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUp : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector2 MoveUpValue = new Vector2 (0,1);
    Vector2 NotMovingValue = new Vector2 (0,0);

    public void OnPointerDown(PointerEventData eventData)
    {

        FindAnyObjectByType<PlayerMovement>().OnMove(MoveUpValue);


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FindAnyObjectByType<PlayerMovement>().OnMove(NotMovingValue);


    }
}
