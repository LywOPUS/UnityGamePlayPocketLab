using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IPointerUpHandler, IDragHandler
{
    public UnityAction onClick;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onPointerUP;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUP != null)
        {
            onPointerUP(eventData);
        }
    }
}