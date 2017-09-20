using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //TODo:添加单例
    public static ObjectPool _Instance;

    public static ObjectPool Instance
    {
        get
        {
            if (_Instance == null)
            {
                var obj = new GameObject("objectPool");
                _Instance = obj.AddComponent<ObjectPool>();
                DontDestroyOnLoad(obj);
            }
            return _Instance;
        }
    }

    public GameObject poolObj;

    //创建字典
    public Dictionary<string, List<object>> pools = new Dictionary<string, List<object>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        poolObj = new GameObject();
        poolObj.name = "ObjectPool";
        DontDestroyOnLoad(poolObj);
    }

    public IEnumerator Delay(float rTime, UnityEngine.Events.UnityAction rFunc)
    {
        yield return new WaitForSeconds(rTime);

        rFunc();
    }

    public void AddRes(string rName, Object rRes)
    {
        if (rRes.GetType() == typeof(GameObject))
        {
            (rRes as GameObject).transform.SetParent(poolObj.transform);
            (rRes as GameObject).SetActive(false);
        }
        if (!pools.ContainsKey(rName))
        {
            pools.Add(rName, new List<object>() { rRes });
        }
        else
        {
            pools[rName].Add(rRes);
        }
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rName"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public T GetRes<T>(string rName, Vector3 pos) where T : Object
    {
        if (pools.ContainsKey(rName))
        {
            var resList = pools[rName];
            int index = resList.Count - 1;

            object obj = resList[index];
            resList.RemoveAt(index);

            if (resList.Count == 0)
            {
                pools.Remove(rName);
            }

            if (typeof(T) == typeof(GameObject))
            {
                var gameObj = ((GameObject)obj);
                gameObj.SetActive(true);
                gameObj.transform.position = pos;
            }
            return obj as T;
        }
        else
        {
            var res = Resources.Load<T>("Prefabs/Scenel" + rName) as GameObject;

            object obj = null;

            if (typeof(T) == typeof(GameObject))
            {
                obj = GameObject.Instantiate(res, pos, Quaternion.identity);
                var gameObj = ((GameObject)obj);
                gameObj.name = rName;
                gameObj.transform.SetParent(poolObj.transform);
            }
            return obj as T;
        }
    }

    public void BackRes(string rName, object rRes)
    {
        GameObject obj = rRes as GameObject;

        if (obj != null)
        {
            obj.SetActive(false);
            obj.transform.SetParent(poolObj.transform);
        }
        if (pools.ContainsKey(rName))
        {
            pools[rName].Add(rRes);
        }
        else
        {
            pools.Add(rName, new List<object>() { rRes });
        }
    }
}