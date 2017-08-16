using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Page_Bag : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;

    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private Button[] buttonArray;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GridLayoutGroup grid;

    private int currentItemNum;

    private void Awake()
    {
        rect = this.gameObject.transform.Find("Canvas/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        grid = rect.GetComponent<GridLayoutGroup>();
        item = Resources.Load("assetsbundles/ui/Com_Item") as GameObject;

        buttonArray = this.gameObject.transform.Find("Canvas/Buttons/").GetComponentsInChildren<Button>();
        buttonArray[0].onClick.AddListener(OnAddItemClick);
        buttonArray[1].onClick.AddListener(OnExitClick);
    }

    private void OnAddItemClick()
    {
        GameObject item = GameObject.Instantiate(this.item) as GameObject;

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
        Destroy(this.gameObject);
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