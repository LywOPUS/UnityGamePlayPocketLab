using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public void Awake()
    {
        RegistManager();
        PreLoadRes();
        EnterGame();
    }

    public void Start()
    {
        Debug.Log(UIManager.instance.GetPageUI<Page_Menu>());
    }

    /// <summary>
    /// 注册管理器
    /// </summary>
    public void RegistManager()
    {
        GameObject manager = new GameObject("manager");
        DontDestroyOnLoad(manager);

        manager.AddComponent<UIManager>();
        manager.AddComponent<ConfigUtil>();

        Debug.Log("成功注册管理器");
    }

    /// <summary>
    /// 预先加载好资源
    /// </summary>
    public void PreLoadRes()
    {
        ConfigUtil.Instance.Init();
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame()
    {
        CreatBgUI();
    }

    private void CreatBgUI()
    {
        UIManager.instance.CreatPageUI<Page_Menu>();
    }
}