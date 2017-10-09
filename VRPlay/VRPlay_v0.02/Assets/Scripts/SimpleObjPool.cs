using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjPool : MonoSingleton<SimpleObjPool>
{
    public GameObject poolobj;

    public Dictionary<string, List<Object>> pools = new Dictionary<string, List<Object>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        poolobj = new GameObject();

        poolobj.name = "poolObj";

        DontDestroyOnLoad(poolobj);
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
            Debug.Log("池中添加" + rName);
            (rRes as GameObject).transform.SetParent(poolobj.transform);
            (rRes as GameObject).SetActive(false);
        }

        if (!pools.ContainsKey(rName))
        {
            Debug.Log("池中添加" + rName);

            pools.Add(rName, new List<Object>() { rRes });
        }
        else
        {
            Debug.Log("其他情况");
            pools[rName].Add(rRes);
        }
    }

    public T GetRes<T>(string rName, Vector3 pos) where T : Object
    {
        if (pools.ContainsKey(rName))
        {
            var resList = pools[rName];

            int index = resList.Count - 1;

            Object obj = resList[index];

            resList.RemoveAt(index);

            if (resList.Count == 0)
            {
                pools.Remove(rName);
            }

            //物体激活
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
            var res = Resources.Load<T>("Prefabs/" + rName) as GameObject;
            if (res == null)
            {
                res = Resources.Load<T>("Prefabs/Terrains/" + rName) as GameObject;
            }
            if (res == null)
            {
                res = Resources.Load<T>("Prefabs/Billboards/" + rName) as GameObject;
            }
            Object obj = null;

            if (typeof(T) == typeof(GameObject))
            {
                obj = GameObject.Instantiate(res, pos, Quaternion.identity);

                var gameObj = ((GameObject)obj);
                gameObj.name = rName;
                gameObj.transform.SetParent(poolobj.transform);
            }
            return obj as T;
        }
    }

    public void BackRes(string rName, Object rRes)
    {
        //物体关闭
        GameObject obj = rRes as GameObject;

        if (obj != null)
        {
            obj.SetActive(false);

            obj.transform.SetParent(poolobj.transform);
        }

        if (pools.ContainsKey(rName))
        {
            pools[rName].Add(rRes);
        }
        else
        {
            pools.Add(rName, new List<Object>() { rRes });
        }
    }
}