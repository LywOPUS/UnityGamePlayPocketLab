using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject UIRoot;

    private Dictionary<string, UIBase> uiPage = new Dictionary<string, UIBase>();

    public T GetUiPage<T>() where T : MonoBehaviour
    {
        string rName = typeof(T).ToString();
        return uiPage[rName] as T;
    }

    public T CreatPage_UI<T>() where T : UIBase
    {
        Debug.Log(typeof(T).ToString());
        string rName = typeof(T).ToString();
        GameObject Pui = Resources.Load<GameObject>("assetsbundles/ui/" + rName) as GameObject;
        if (Pui != null)
        {
            Pui = GameObject.Instantiate(Pui);
            Pui.gameObject.transform.SetParent(UIRoot.transform);
            T pageScript = Pui.AddComponent<T>();
            uiPage.Add(rName, pageScript);
            return pageScript as T;
        }
        else
        {
            Debug.Log("Can't find " + rName + "at assetsbundles/ui/");
            return null;
        }
    }

    public T CreatCom_item<T>(Transform rParent) where T : UIBase
    {
        Debug.Log("ComItem: " + typeof(T).ToString());
        string rName = typeof(T).ToString();
        GameObject item = Resources.Load<GameObject>("assetsbundles/ui/" + rName);
        item = GameObject.Instantiate(item, rParent);
        item.name = rName;
        item.gameObject.transform.localScale = Vector3.one;

        T script = item.AddComponent<T>();
        return script;
    }

    public void ClosePage<T>()
    {
        string rName = typeof(T).Name;
        Destroy(uiPage[rName].gameObject);
        uiPage.Remove(rName);
    }

    private void Init()
    {
        UIRoot = Resources.Load<GameObject>("local/systemres/UIroot");
        if (UIRoot != null)
        {
            UIRoot.name = "UIroot";
            UIRoot = GameObject.Instantiate(UIRoot);
        }
        else
        {
            Debug.Log("Can't find UIroot in 'Resources/local/systemres/'");
        }

        DontDestroyOnLoad(UIRoot);
    }

    private void Awake()
    {
        instance = this;
        Init();
    }
}