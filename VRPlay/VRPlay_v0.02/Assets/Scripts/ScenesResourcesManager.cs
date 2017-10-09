using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesResourcesManager : MonoSingleton<ScenesResourcesManager>
{
    public int TerriansCount;

    /// <summary>
    /// TerrianPwanInit
    /// </summary>
    public void Init()
    {
        //ToDo:加载地形块；
        FindStreet();
        //ToDo:加载道路；
        FindTerrain();
        //ToDo：加载路牌；
        //ToDo:加载广告牌；
    }

    /// <summary>
    ///在世界原点后方生成地形块
    /// </summary>
    public void InsTerrianinStart(Vector3 pos)
    {
        foreach (var item in SimpleObjPool.Instance.pools)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }
        var posZ = pos.z;
        var size = -TerriansCount;
        for (int i = 0; i < TerriansCount; i++)
        {
            var obj = SimpleObjPool.Instance.GetRes<GameObject>("Terrain_A0" + (i + 1), new Vector3(0, 0, posZ + size * 100));
            //SimpleObjPool.Instance.BackRes("Terrain_A0" + (i + 1), obj);
            var st = SimpleObjPool.Instance.GetRes<GameObject>("Street_01", new Vector3(0, 0, posZ + size * 100));
            //SimpleObjPool.Instance.BackRes("Street_01", st);

            size++;
        }
        Debug.Log("初始地形生成成功");
    }

    /// <summary>
    /// 更新地形快位置
    /// </summary>

    public void UpdateTerrain(int size, Vector3 pos)
    {
        //var posZ = pos.z;
        //for (int i = 0; i < size; i++)
        //{
        //    //ObjectPool.Instance.GetRes("Terrian_A0"+i,posZ+i*100);

        //    //var street = ObjectPool.Instance.RequestCacheGameObejct(ObjectPool.Instance.Streets[i]);
        //    //street.SetActive(true);

        //    //Terrian.SetActive(true);

        //    //Terrian.transform.position = new Vector3(0, 0, posZ + i * 100);
        //    //street.transform.position = new Vector3(0, 0, posZ + i * 100);
        //}
    }

    /// <summary>
    /// 随机列表发生器
    /// </summary>
    /// <param name="targetList"></param>
    /// <returns></returns>
    public List<GameObject> RanddomList(List<GameObject> targetList)
    {
        var tampArray = new GameObject[targetList.Count];
        targetList.ToArray();
        for (int i = 0; i < tampArray.Length; i++)
        {
        }
        var tempList = new List<GameObject>();

        return tempList;
    }

    /// <summary>
    /// 更新碰撞盒
    /// </summary>
    //public void UpdateLoadNext(int count, Vector3 pos)
    //{
    //    Instantiate(LoadNext, new Vector3(0, 0, this.transform.position.z + count / 2 * 100), Quaternion.identity);
    //}

    /// <summary>
    /// 加载地形块
    /// </summary>
    public List<GameObject> FindTerrain()
    {
        var tempList = new List<GameObject>();
        //for (int j = 0; j < 3; j++)
        //{
        for (int i = 0; i < TerriansCount; i++)
        {
            if (i < 9)
            {
                var obj = Resources.Load("Prefabs/Terrains/Terrain" + "_A0" + (i + 1)) as GameObject;

                Debug.Log(obj.name);
                tempList.Add(obj);
                //SimpleObjPool.Instance.AddRes("Terrain_A0" + (i + 1), obj);
                //  Debug.Log(SimpleObjPool.Instance.pools[obj.name].ToString());

                Debug.Log("地形资源申请成功");
            }
            else if (i >= 9)
            {
                var obj = (Resources.Load("Prefabs/Terrains/Terrain" + "_A" + (i + 1)) as GameObject);
                // SimpleObjPool.Instance.AddRes(obj.name, obj);
                Debug.Log("地形资源资源申请成功");
            }
        }
        //}
        return tempList;
    }

    public List<GameObject> ReturnInstantianteTerrian()
    {
        var tempList = new List<GameObject>();
        for (int i = 0; i < TerriansCount; i++)
        {
            if (i < 9)
            {
                var obj = Instantiate(Resources.Load("Prefabs/Terrains/Terrain" + "_A0" + (i + 1)) as GameObject);

                Debug.Log(obj.name);
                tempList.Add(obj);
                //SimpleObjPool.Instance.AddRes("Terrain_A0" + (i + 1), obj);
                //  Debug.Log(SimpleObjPool.Instance.pools[obj.name].ToString());

                Debug.Log("地形资源申请成功");
            }
            else if (i >= 9)
            {
                var obj = (Resources.Load("Prefabs/Terrains/Terrain" + "_A" + (i + 1)) as GameObject);
                Debug.Log(obj.name);
                tempList.Add(obj);

                // SimpleObjPool.Instance.AddRes(obj.name, obj);
                Debug.Log("地形资源资源申请成功");
            }
        }
        return tempList;
    }

    /// <summary>
    /// 加载道路
    /// </summary>
    /// <returns>返回一个道路预制体</returns>
    public List<GameObject> FindStreet()
    {
        var tempList = new List<GameObject>();
        for (int i = 0; i < TerriansCount; i++)
        {
            var obj = Resources.Load("Prefabs/Street_01") as GameObject;
            Debug.Log(obj.name);
            tempList.Add(obj);
            // SimpleObjPool.Instance.AddRes(obj.name, obj);
            // SimpleObjPool.Instance.BackRes("Street_01", obj);

            Debug.Log("道路资源申请加载成功");
        }
        return tempList;
    }

    public List<GameObject> ReturnInstantaceStreet()
    {
        var tempList = new List<GameObject>();
        for (int i = 0; i < TerriansCount; i++)
        {
            var obj = Resources.Load("Prefabs/Street_01") as GameObject;
            tempList.Add(obj);
        }
        return tempList;
    }

    public GameObject FindLoadNext()
    {
        var LoadNext = Resources.Load("Prefabs/LoadNext") as GameObject;
        return LoadNext;
    }

    public GameObject ReturnInstantiateLoadNex()
    {
        var LoadNext = Instantiate(Resources.Load("Prefabs/LoadNext")) as GameObject;
        return LoadNext;
    }
}