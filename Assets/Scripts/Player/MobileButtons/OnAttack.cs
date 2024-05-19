using UnityEngine;
using UnityEngine.EventSystems;

public class OnAttack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEvent)
    {
        FindAnyObjectByType<PlayerAttack>().PullBow();
    }

    public void OnPointerUp(PointerEventData pointerEvent)
    {
        return;
    }

    
}
