using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public void Awake()
    {
        GameObject manager = new GameObject("manager");
        manager.AddComponent<UIManager>();
        DontDestroyOnLoad(manager);
        GameObject menu = UIManager.instance.CreatPage_UI("Page_Menu");
        if (menu != null)
        {
            menu.AddComponent<Page_Menu>();
        }
        else
        {
            Debug.Log("menu can't find");
        }
    }
}