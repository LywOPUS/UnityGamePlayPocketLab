using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Page_Bag : UIBase
{
    public RectTransform rectTran;

    public Transform transPage;

    private Button[] buttonArray;

    private GridLayoutGroup grid;

    private int currentItemNum;

    private void Start()
    {
        //Todo: 查找物体

        //Todo: 按钮添加监听事件

        //Todo: 读取资源
        transPage = this.gameObject.transform.Find("Canvas/TransPage");
        if (transPage == null)
        {
            Debug.Log("Can't find transPage");
        }
        rectTran = this.gameObject.transform.Find("Canvas/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        grid = rectTran.GetComponent<GridLayoutGroup>();

        buttonArray = this.gameObject.transform.Find("Canvas/Buttons/").GetComponentsInChildren<Button>();

        SetEventTrigger(buttonArray[0].gameObject).onClick = OnClosePageUI;
        SetEventTrigger(buttonArray[1].gameObject).onClick = AddItem;
        SetEventTrigger(buttonArray[2].gameObject).onClick = AddItem1;
        SetEventTrigger(buttonArray[3].gameObject).onClick = AddItem2;

        Init();
    }

    private Com_Item CreatItem()
    {
        var com_Item = UIManager.instance.CreatComUI<Com_Item>(rectTran);

        currentItemNum++;
        ChangeRectHight();
        return com_Item;
    }

    /// <summary>
    /// 加载持久化数据
    /// </summary>
    private void Init()
    {
        foreach (var info in BagData.Instance.curItemDict)
        {
            for (int i = 0; i < info.Value.count; i++)
            {
                var item = CreatItem();
                item.data = info.Value;
            }
        }
    }

    //Todo: 创建具体物体
    private void AddItem()
    {
        var item = CreatItem();
        item.data = BagData.Instance.curItemDict["0000"];
        BagData.Instance.curItemDict["0000"].count++;
    }

    private void AddItem1()
    {
        var item = CreatItem();
        item.data = BagData.Instance.curItemDict["0001"];
        BagData.Instance.curItemDict["0001"].count++;
    }

    private void AddItem2()
    {
        var item = CreatItem();
        item.data = BagData.Instance.curItemDict["0002"];
        BagData.Instance.curItemDict["0002"].count++;
    }

    #region Old

    private void OnClosePageUI()
    {
        UIManager.instance.ClosePageUI<Page_Bag>();
    }

    private void ChangeRectHight()
    {
        if (grid != null)
        {
            float itemHight = grid.spacing.y + grid.spacing.x;
            rectTran.sizeDelta = new Vector2(0, itemHight * Mathf.Ceil((float)currentItemNum / grid.constraintCount));
        }
        else
        {
            Debug.Log("Can't find gird in rect");
        }
    }

    #endregion Old
}