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
        Page_Menu menu = UIManager.instance.CreatPage_UI<Page_Menu>();
        Debug.Log(UIManager.instance.GetUiPage<Page_Menu>());
    }
}