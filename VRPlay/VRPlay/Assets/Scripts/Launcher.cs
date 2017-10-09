using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    //public static int TerrianCount = 6;
    public int TerrianCount = 6;

    public GameObject LoadingLogo;

    public void Awake()
    {
        RegistManager();
    }

    public void Start()
    {
        PreLoadRes();
        NatureScenesPwan.Instance.Init(new Vector3(0, 0, 0), TerrianCount);
        NatureScenesPwan.Instance.UpdateSelf(new Vector3(0, 0, 0), 6, 1);
        NatureScenesPwan.Instance.LoadNext.transform.position = new Vector3(0, 0, -300);
    }

    /// <summary>
    /// 注册管理器
    /// </summary>
    public void RegistManager()
    {
        GameObject manager = new GameObject("manager");
        DontDestroyOnLoad(manager);

        Debug.Log("成功注册管理器");
    }

    //资源预加载
    public void PreLoadRes()
    {
        ScenesResourcesManager.Instance.TerriansCount = this.TerrianCount;
        ScenesResourcesManager.Instance.Init();

        Debug.Log("加载地形资源");
    }

    public void InitBasePos()
    {
    }

    //进入游戏
    public void EnterGame()
    {
        // Resources.Load("Prefabs/")
    }
}