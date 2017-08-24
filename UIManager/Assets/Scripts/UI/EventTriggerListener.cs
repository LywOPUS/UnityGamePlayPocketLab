using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class EventTrigerLisner : MonoBehaviour, IPointerUpHandler, IDragHandler, IBeginDragHandler, IPointerClickHandler
{
    public UnityAction onClick;
    public UnityAction<string> onButtonsClick;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onPointerUp;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null)
        {
            onPointerUp(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData
        )
    {
        if (onClick != null)
        {
            onClick();
        }
    }
}