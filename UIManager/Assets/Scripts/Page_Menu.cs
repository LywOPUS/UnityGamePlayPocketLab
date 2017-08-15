using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Menu : MonoBehaviour
{
    private Button[] ButtonArry;

    private void Awake()
    {
        ButtonArry = gameObject.transform.Find("Canvas/MenuButtons").GetComponentsInChildren<Button>();
        if (ButtonArry[0] != null)
        {
            ButtonArry[0].onClick.AddListener(LunchBagPack);
        }
        else Debug.Log("Can't find BAGPACK ");
        Debug.Log(ButtonArry[0].gameObject.name);
    }

    private void LunchBagPack()
    {
        GameObject pag = UIManager.instance.CreatPage_UI("Page_Bag");
        pag.AddComponent<Page_Bag>();
    }
}