using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private void Awake()
    {
        RegistManager();
        PreLoadRes();
        EnterGame();
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    private void EnterGame()
    {
        CreatStartUI();
    }

    /// <summary>
    /// 预加载资源
    /// </summary>
    private void PreLoadRes()
    {
        Debug.Log("加载资源");
    }

    /// <summary>
    /// 注册管理器
    /// </summary>
    private void RegistManager()
    {
        GameObject managerObj = new GameObject("Manager");
        DontDestroyOnLoad(managerObj);
        managerObj.AddComponent<UIManager>();
        managerObj.AddComponent<ConfigUtil>();
    }

    /// <summary>
    /// 创建开始界面
    /// </summary>
    private void CreatStartUI()
    {
        UIManager.Instance.CreatPageUI<Page_Start>();
    }
}