using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject Com_Item;

    public RectTransform rectTran;

    public Button Btn_AddItem;

    public GridLayoutGroup grid;

    public int currentItemNum;

    void Start()
    {
        Btn_AddItem.onClick.AddListener(AddItem);
        Com_Item = Resources.Load("assetsbundles/ui/Com_Item") as GameObject;
    }

    public void AddItem()
    {
        var obj = GameObject.Instantiate(Com_Item) as GameObject;
        obj.transform.parent = rectTran;
        obj.gameObject.transform.localScale = Vector3.one;
        currentItemNum++;

        ChangeHight();
    }

    public void ChangeHight()
    {
        float itemHight = grid.spacing.y + grid.cellSize.y;

        rectTran.sizeDelta = new Vector2(0, itemHight * Mathf.Ceil((float)currentItemNum / grid.constraintCount));
    }
}