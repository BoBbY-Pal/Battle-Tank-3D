using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        EventHandler.Instance.InvokeFireButtonPressedEvent();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventHandler.Instance.InvokeFireButtonReleasedEvent();
    }
}
