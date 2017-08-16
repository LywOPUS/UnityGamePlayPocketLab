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
    private bool isPass;
    private bool isClick;

    private void Awake()
    {
        item = this.gameObject;
        initPos = item.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPass)
        {
            return;
        }
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
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Desthory")
        {
            Destroy(this.gameObject);
        }

        gameObject.transform.localPosition = initPos;
    }

    private void OnDestroy()
    {
        Debug.Log("This gameobject was Destroy");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = true;

        isClick = false;
    }
}