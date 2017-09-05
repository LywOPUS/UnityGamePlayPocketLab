using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject UIRoot;

    public Camera UICamera;
    private Dictionary<string, UIBase> uiDict = new Dictionary<string, UIBase>();

    public void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        UIRoot = Resources.Load<GameObject>("local/systemres/UIRoot");
        if (UIRoot != null)
        {
            UIRoot = GameObject.Instantiate(UIRoot);
            UIRoot.name = "UIRoot";
            UICamera = UIRoot.transform.Find("UICamera").GetComponent<Camera>();

            DontDestroyOnLoad(UIRoot);
        }
    }

    /// <summary>
    /// 获取界面UI
    /// </summary>
    public T GetPageUI<T>() where T : UIBase
    {
        string rName = typeof(T).Name;

        if (!uiDict.ContainsKey(rName))
        {
            Debug.LogError("当前UI未加载" + rName);
            return null;
        }
        return uiDict[rName] as T;
    }

    /// <summary>
    /// 创建界面
    /// </summary>
    public T CreatPageUI<T>() where T : UIBase
    {
        string rName = typeof(T).Name;
        GameObject uiObj = Resources.Load<GameObject>("assetsbundles/ui/" + rName);
        if (uiObj == null)
        {
            Debug.LogError(rName + "资源不存在");
            return null;
        }
        //实例化
        uiObj = GameObject.Instantiate(uiObj, UIRoot.transform);
        uiObj.name = rName;

        //初始化
        uiObj.transform.SetParent(UIRoot.transform);
        uiObj.transform.localScale = Vector3.one;

        //添加相机
        var canvas = uiObj.transform.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = UICamera;

        var menuScript = uiObj.AddComponent<T>();

        uiDict.Add(rName, menuScript);

        return menuScript as T;
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public void ClosePageUI<T>()
    {
        string rName = typeof(T).Name;

        Destroy(uiDict[rName].gameObject);
        uiDict.Remove(rName);
    }

    /// <summary>
    /// 创建通用UI
    /// </summary>
    /// <typeparam name="T">资源名</typeparam>
    /// <param name="rParent">父节点</param>
    /// <returns></returns>
    public T CreatComUI<T>(Transform rParent) where T : UIBase
    {
        string rName = typeof(T).Name;

        GameObject uiObj = Resources.Load<GameObject>("assetsbundles/ui/" + rName);
        if (uiObj == null)
        {
            Debug.LogError(rName + "资源不存在");
            return null;
        }

        uiObj = GameObject.Instantiate(uiObj, rParent);
        uiObj.name = rName;
        uiObj.gameObject.transform.localScale = Vector3.one;

        var script = uiObj.AddComponent<T>();
        return script;
    }
}