using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// 在开始界面加载所有的文件到场景中？
    /// 用事件管理所有的ui事件。
    ///
    ///
    /// </summary>
    private Button AddWepon;

    private RectTransform UICanvas;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

//资源加载
public class Item
{
    private GameObject Wepon;
    private GameObject Armor;
    private GameObject sundry;
    //构造函数
}