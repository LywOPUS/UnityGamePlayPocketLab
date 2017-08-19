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
        Debug.Log(UIManager.instance.GetUiPage<Page_Menu>());
    }

    /// <summary>
    /// 注册管理器
    /// </summary>
    public void RegistManager()
    {
    }

    /// <summary>
    /// 预先加载好资源
    /// </summary>
    public void PreLoadRes()
    {
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame()
    {
    }

    private void CreatBgUI()
    {
        UIManager.instance.CreatPage_UI<Page_Menu>();
    }
}