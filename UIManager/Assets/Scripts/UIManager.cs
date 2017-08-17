using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject UIRoot;

	private Dictionary<string,GameObject> UIList = new Dictionary<string, GameObject> ();

    public GameObject CreatPage_UI(string rName)
    {
        GameObject PUi = Resources.Load<GameObject>("assetsbundles/ui/" + rName);
        if (PUi != null)
        {
            PUi = GameObject.Instantiate(PUi);
            PUi.gameObject.transform.SetParent(UIRoot.transform);
        }
        else
        {
            Debug.Log("Can't find " + rName + "at assetsbundles/ui/");
        }

        return PUi;
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
        //DontDestroyOnLoad(this);
    }
}