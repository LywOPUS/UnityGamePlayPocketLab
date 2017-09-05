using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Start : UIBase
{
    private Image start;

    private void Start()
    {
        start = gameObject.transform.Find("Start").GetComponent<Image>();
        SetEventTrigger(start.gameObject).onClick = OnStartClick;
    }

    private void OnStartClick()
    {
        UIManager.Instance.ClosePageUI<Page_Start>();
        if (UIManager.Instance.GetPageUI<Page_Main>() != null)
        {
            Debug.Log("Main UI已经创建");
            return;
        }
        UIManager.Instance.CreatPageUI<Page_Main>();
    }
}