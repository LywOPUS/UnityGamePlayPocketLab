using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject UIRoot;

    private Dictionary<string, UIBase> uiPageDict = new Dictionary<string, UIBase>();

    /// <summary>
    /// 关闭
    /// </summary>
    public void ClosePageUI<T>()
    {
        string rName = typeof(T).Name;

        Destroy(uiPageDict[rName].gameObject);
        uiPageDict.Remove(rName);
    }

    /// <summary>
    /// 创建一个物品UI
    /// </summary>
    public T CreatComUI<T>(Transform rParent) where T : UIBase
    {
        string rName = typeof(T).ToString();
        GameObject UiGoItem = Resources.Load<GameObject>("assetsbundles/ui/" + rName);

        if (UiGoItem == null)
        {
            Debug.Log(rName + "资源不存在");
            return null;
        }

        //实例化
        UiGoItem = GameObject.Instantiate(UiGoItem, rParent);
        UiGoItem.name = rName;
        UiGoItem.gameObject.transform.localScale = Vector3.one;

        Debug.Log("创建了: " + typeof(T).ToString() + "视图");

        T script = UiGoItem.AddComponent<T>();

        return script;
    }

    /// <summary>
    /// 创建并实例化一个UI界面
    /// </summary>
    public T CreatPageUI<T>() where T : UIBase
    {
        string rName = typeof(T).ToString();
        GameObject UiGoPage = Resources.Load<GameObject>("assetsbundles/ui/" + rName);

        if (UiGoPage == null)
        {
            Debug.Log("Can't find " + rName + "at assetsbundles/ui/");
            return null;
        }

        //实例化
        UiGoPage = GameObject.Instantiate(UiGoPage, UIRoot.transform);
        UiGoPage.name = rName;
        UiGoPage.transform.SetParent(UIRoot.transform);
        UiGoPage.transform.localScale = Vector3.one;

        Debug.Log("创建了" + typeof(T).ToString() + "界面");

        T pageScript = UiGoPage.AddComponent<T>();
        uiPageDict.Add(rName, pageScript);

        return pageScript as T;
    }

    /// <summary>
    ///获取ui界面
    /// </summary>
    public T GetPageUI<T>() where T : MonoBehaviour
    {
        string rName = typeof(T).ToString();
        if (!uiPageDict.ContainsKey(rName))
        {
            Debug.LogError("当前UI未加载" + rName);
            return null;
        }
        return uiPageDict[rName] as T;
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        UIRoot = Resources.Load<GameObject>("local/systemres/UIroot");
        if (UIRoot != null)
        {
            UIRoot.name = "UIroot";
            UIRoot = GameObject.Instantiate(UIRoot);

            DontDestroyOnLoad(UIRoot);
        }
    }
}