using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Page_Bag : UIBase
{
    [SerializeField]
    public RectTransform rect;

    public Transform transPage;

    [SerializeField]
    private Button[] buttonArray;

    [SerializeField]
    private GridLayoutGroup grid;

    private int currentItemNum;

    private void Start()
    {
        transPage = this.gameObject.transform.Find("Canvas/TransPage");
        if (transPage == null)
        {
            Debug.Log("Can't find transPage");
        }
        rect = this.gameObject.transform.Find("Canvas/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        grid = rect.GetComponent<GridLayoutGroup>();

        buttonArray = this.gameObject.transform.Find("Canvas/Buttons/").GetComponentsInChildren<Button>();

        SetEventTrigger(buttonArray[0].gameObject).onClick = OnAddItemClick;
        SetEventTrigger(buttonArray[1].gameObject).onClick = OnExitClick;
    }

    private void OnAddItemClick()
    {
        // GameObject item = GameObject.Instantiate(this.item) as GameObject;
        Com_Item item = UIManager.instance.CreatCom_item<Com_Item>(rect);
        if (item == null)
        {
            Debug.Log("item is null");
        }
        if (rect != null)
        {
            Debug.Log("rect isn't null");
            item.gameObject.transform.SetParent(rect);
            Transform trans = item.transform.Find("Transform_darg");
            trans.gameObject.AddComponent<DargItem>();
        }
        else
        {
            Debug.Log("Can't find item's parent");
        }
        currentItemNum++;
        ChangeRectHight();
    }

    private void OnExitClick()
    {
        UIManager.instance.ClosePageUI<Page_Bag>();
    }

    private void ChangeRectHight()
    {
        if (grid != null)
        {
            float itemHight = grid.spacing.y + grid.spacing.x;
            rect.sizeDelta = new Vector2(0, itemHight * Mathf.Ceil((float)currentItemNum / grid.constraintCount));
        }
        else
        {
            Debug.Log("Can't find gird in rect");
        }
    }
}