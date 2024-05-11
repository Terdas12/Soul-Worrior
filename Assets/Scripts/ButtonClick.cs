using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public UnityEvent PointerDown;
    public UnityEvent PointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp.Invoke();
    }
}
