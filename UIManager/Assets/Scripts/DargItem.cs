using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DargItem : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerClickHandler, IBeginDragHandler
{
    private GameObject item;
    private Vector3 initPos;
    private Transform currentParent;
    public Transform Destory;
    public Transform DestoryCurrentParent;

    private void Start()
    {
        // Debug.Log(DargParent.gameObject.name);
        Destory = UIManager.instance.GetUiPage<Page_Bag>().transform.Find("Canvas/Destroy");

        if (Destory == null)
        {
            Debug.Log("Can't find Destoty");
        }
        DestoryCurrentParent = Destory.parent.transform;
        currentParent = this.gameObject.transform.parent;
        initPos = this.transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentParent.SetParent(UIManager.instance.GetUiPage<Page_Bag>().transPage);
        Destory.SetParent(UIManager.instance.GetUiPage<Page_Bag>().transPage);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.gameObject != null)
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
            //  currentParent.SetParent(UIManager.instance.GetUiPage<Page_Bag>().rect);
            Destroy(currentParent.gameObject);
        }
        else
        {
            Debug.Log("Can't Find Destroy");
            currentParent.SetParent(UIManager.instance.GetUiPage<Page_Bag>().rect);
            gameObject.transform.localPosition = initPos;
            // gameObject.transform.localPosition = initPos;
        }
        Destory.SetParent(DestoryCurrentParent);
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