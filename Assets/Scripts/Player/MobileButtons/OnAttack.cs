using UnityEngine;
using UnityEngine.EventSystems;

public class OnAttack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] PlayerAttack playerAttack;

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
        playerAttack.PullBow();
    }

    public void OnPointerUp(PointerEventData pointerEvent)
    {
        return;
    }

    
}
