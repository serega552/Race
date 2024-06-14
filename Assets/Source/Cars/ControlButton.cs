using UnityEngine;
using UnityEngine.EventSystems;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHold { get; private set; } = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHold = false;
    }
}
