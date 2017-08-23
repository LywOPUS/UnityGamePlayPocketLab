using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DargItem : UIBase
{
    private Vector3 initPos;
    private Transform currentParent;
    public Transform Destory;
    public Transform DestoryCurrentParent;

    private void Start()
    {
        Destory = UIManager.instance.GetPageUI<Page_Bag>().transform.Find("Canvas/Destroy");
        if (Destory == null)
        {
            Debug.Log("Can't find Destoty");
        }

        SetEventTrigger(this.gameObject).onBeginDrag = OnBeginDrag;
        SetEventTrigger(this.gameObject).onPointerUp = OnPointerUp;
        SetEventTrigger(this.gameObject).onDrag += OnDrag;
        DestoryCurrentParent = Destory.parent.transform;
        currentParent = this.gameObject.transform.parent;
        initPos = this.transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentParent.SetParent(UIManager.instance.GetPageUI<Page_Bag>().transPage);
        Destory.SetParent(UIManager.instance.GetPageUI<Page_Bag>().transPage);
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
            Destroy(currentParent.gameObject);
            BagData.Instance.curItemDict[this.gameObject.GetComponentInParent<Com_Item>().data.id].count--;
        }
        else
        {
            Debug.Log("Can't Find Destroy");
            currentParent.SetParent(UIManager.instance.GetPageUI<Page_Bag>().rectTran);
            gameObject.transform.localPosition = initPos;
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