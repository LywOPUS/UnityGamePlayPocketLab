using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StPool : MonoBehaviour
{
    public static Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();

    public static GameObject GetObj(string prefabName, Vector3 position, Quaternion rotaion)
    {
        var go = (GameObject)Instantiate(Resources.Load("perfabs/" + prefabName), position, rotaion);
        string gameobjname = prefabName + "(clone)";
        if (dic.ContainsKey(gameobjname) && dic[gameobjname].Count > 0)
        {
            Debug.Log("池中存在此物体");
            ArrayList tempList = dic[gameobjname];
            go = (GameObject)tempList[0];
            tempList.RemoveAt(0);
            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotaion;
        }
        return go;
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static GameObject ReturnObj(GameObject obj)
    {
        string key = obj.name;
        if (dic.ContainsKey(key))
        {
            dic[key].Add(obj);
        }
        else
        {
            dic[key] = new ArrayList() { obj };
        }
        obj.SetActive(false);
        return obj;
    }
}