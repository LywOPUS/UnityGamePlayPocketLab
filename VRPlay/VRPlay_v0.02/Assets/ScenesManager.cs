using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoSingleton<ScenesManager>
{
    //加载权重
    public float LoadSceneTargetValue = 0;

    public float LoadResTargetValue = 0;
    public float nullSceneTargetValue = 0;
    private float tragetValue;
    public UnityEngine.Events.UnityAction LoadOverEvent;

    /// public TextMesh loadingText;

    private void Start()
    {
        //loadingText = new TextMesh();

        // loadingText = GameObject.Instantiate(loadingText, new Vector3(0, 0, 0), Quaternion.identity);
        //loadingText.gameObject.name = "loadingText";
    }

    public void Init()
    {
        var Loding = Resources.Load<GameObject>("Prefabs/Loading");
        Instantiate(Loding, new Vector3(0, 1, 0), Quaternion.identity);
        //StartCoroutine(LoadNullScene(SceneName));
        //if (nullSceneTargetValue == 1)
        //{
        //    StartCoroutine(LoadRes(SceneName));
        //    if (LoadResTargetValue == 1)
        //    {
        //        StartCoroutine(LoadScene(SceneName));
        //        if (LoadSceneTargetValue >= 1)
        //        {
        //            StopAllCoroutines();
        //        }
        //    }
        //}
        StartCoroutine(LoadRes());
    }

    public IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            LoadSceneTargetValue = op.progress;
            yield return null;
        }
        op.allowSceneActivation = true;
        while (op.progress < 1)
        {
            LoadSceneTargetValue = op.progress;
            yield return null;
        }
        //tragetValue = nullSceneTargetValue * 0.2f + LoadSceneTargetValue + 0.4f + LoadSceneTargetValue * 0.4f;
        //loadingText.text = ((int)tragetValue).ToString() + "%";
        LoadSceneTargetValue = 1;
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadRes()
    {
        var resList = new List<GameObject>();

        var StreetList = new List<GameObject>();
        var TerrainList = new List<GameObject>();

        //
        StreetList = ScenesResourcesManager.Instance.FindStreet();
        TerrainList = ScenesResourcesManager.Instance.FindTerrain();
        var Loadnext = ScenesResourcesManager.Instance.FindLoadNext();

        resList.AddRange(StreetList);
        resList.AddRange(TerrainList);
        resList.Add(Loadnext);

        int addResCount = 0;

        while (addResCount < resList.Count)
        {
            var obj = GameObject.Instantiate(resList[addResCount]);
            if (resList[addResCount].GetType() == typeof(GameObject))
            {
                Debug.Log("添加" + resList[addResCount]);
                // obj.name = obj.name.Replace("(Clone)", "");
                SimpleObjPool.Instance.AddRes(obj.name, obj);
            }
            else
            {
                Debug.Log("2.添加" + resList[addResCount]);

                SimpleObjPool.Instance.AddRes(obj.name, obj);
            }
            addResCount++;
        }
        LoadResTargetValue = (float)addResCount / resList.Count;
        yield return null;

        LoadResTargetValue = 1;

        Debug.Log("资源加载完成");
    }

    //加载空场景
    private IEnumerator LoadNullScene(string sceneName)
    {
        var async = SceneManager.LoadSceneAsync("Loading");
        //场景激活关闭
        async.allowSceneActivation = false;
        //当场景加载小于0.9时
        while (async.progress < 0.9f)
        {
            //空场景的加载进度等于场景的场景的加载进度
            nullSceneTargetValue = async.progress;
            //等待帧结束
            yield return null;
        }
        //场景加载进度大于0.9是，场景激活开启
        async.allowSceneActivation = true;
        //场景进度加载大于0.9，小于1
        while (async.progress < 1)
        {
            nullSceneTargetValue = async.progress;
            yield return null;
        }
        //场景加载进度等于1。空场景等于场景加载进度
        nullSceneTargetValue = 1;
        //开始加载资源
    }
}