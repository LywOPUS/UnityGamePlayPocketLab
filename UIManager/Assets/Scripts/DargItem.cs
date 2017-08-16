using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DargItem : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerClickHandler
{
    private GameObject item;
    private Vector3 initPos;
    private bool isClick;

    private void Awake()
    {
        // Debug.Log(DargParent.gameObject.name);

        item = this.gameObject;
        initPos = item.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            gameObject.transform.position = eventData.position;
        }
        else
        {
            Debug.Log("item is null");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Destroy")
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
        else
        {
            gameObject.transform.localPosition = initPos;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("This gameobject was Destroy");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }
}