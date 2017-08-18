using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Menu : UIBase
{
    private Button[] ButtonArry;

    private void Awake()
    {
        ButtonArry = gameObject.transform.Find("Canvas/MenuButtons").GetComponentsInChildren<Button>();
        if (ButtonArry[0] != null)
        {
            SetEventTrigger(ButtonArry[0].gameObject).onClick = LunchBagPack;
        }
        else Debug.Log("Can't find BAGPACK ");
        Debug.Log(ButtonArry[0].gameObject.name);
    }

    private void LunchBagPack()
    {
        Page_Bag pag = UIManager.instance.CreatPage_UI<Page_Bag>();
    }
}