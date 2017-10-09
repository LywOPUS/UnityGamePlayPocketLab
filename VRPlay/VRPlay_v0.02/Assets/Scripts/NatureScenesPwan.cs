using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureScenesPwan : MonoSingleton<NatureScenesPwan>
{
    public GameObject LoadNext;

    public void Init(Vector3 Pos, int count)
    {
        this.transform.position = Pos;

        LoadNext = ScenesResourcesManager.Instance.FindLoadNext();
        ScenesResourcesManager.Instance.InsTerrianinStart(this.transform.position);
        Debug.Log("资源卵已生成");
    }

    public IEnumerator eUpdateSelf(Vector3 pos, int count, int upsize)
    {
        this.transform.position = pos;

        for (int i = 0; i < upsize; i++)
        {
            var newPos = new Vector3(0, 0, this.transform.position.z + i * 100);
            SimpleObjPool.Instance.GetRes<GameObject>("Terrain_A0" + Random.Range(1, count), newPos);
            SimpleObjPool.Instance.GetRes<GameObject>("Street_01", newPos);
            yield return new WaitForSeconds(0.01f);
        }

        // LoadNext.transform.position += new Vector3(0, 0, this.transform.position.z + upsize / 2 * 100);
        Debug.Log("位置已更新");
        yield return new WaitForSeconds(0.01f);
    }

    public List<GameObject> UpdateSelf(Vector3 pos, int count, int upsize)
    {
        List<GameObject> TempList = new List<GameObject>();
        this.transform.position += pos;

        for (int i = 0; i < upsize; i++)
        {
            var newPos = new Vector3(0, 0, this.transform.position.z + i * 100);
            SimpleObjPool.Instance.GetRes<GameObject>("Terrain_A0" + Random.Range(1, count), newPos);
            SimpleObjPool.Instance.GetRes<GameObject>("Street_01", newPos);
        }

        // LoadNext.transform.position += new Vector3(0, 0, this.transform.position.z + upsize / 2 * 100);
        Debug.Log("位置已更新");
        return TempList;
    }

    //public IEnumerator UpdateSelf(Vector3 pos)
    //{
    //    //this.transform.position = pos;
    //    ////ScenesResourcesManager.Instance.UpdateTerrain(count, this.transform.position);
    //    //var tempListIndex = new int[count];
    //    //for (int i = 0; i < ObjectPool.Instance.Terrians.Count; i++)
    //    //{
    //    //    ObjectPool.Instance.ReturnCacheGameObejct(ObjectPool.Instance.Terrians[i]);
    //    //    Debug.Log("资源回收成功" + ObjectPool.Instance.Terrians[i].name);
    //    //    yield return new WaitForSeconds(0.3f);
    //    //}
    //    //for (int i = 0; i < tempListIndex.Length; i++)
    //    //{
    //    //    tempListIndex[i] = Random.Range(0, count);
    //    //}
    //    //for (int i = 0; i < upsize; i++)
    //    //{
    //    //    if (ObjectPool.Instance.Terrians[tempListIndex[i]] == null)
    //    //    {
    //    //        Debug.Log("池中没有此物");
    //    //    }

    //    //    var vTerrian = ObjectPool.Instance.RequestCacheGameObejct(ObjectPool.Instance.Terrians[tempListIndex[i]]);

    //    //    var vStreet = ObjectPool.Instance.RequestCacheGameObejct(ObjectPool.Instance.Streets[tempListIndex[i]]);

    //    //    vTerrian.transform.position = new Vector3(0, 0, this.transform.position.z + i * 100);
    //    //    vStreet.transform.position = new Vector3(0, 0, this.transform.position.z + i * 100);

    //    //    vTerrian.SetActive(true);
    //    //    vStreet.SetActive(true);

    //    //    Debug.Log("资源设置成功" + vTerrian.name);
    //    //    Debug.Log("资源设置成功" + vStreet.name);
    //    //    yield return new WaitForSeconds(0.3f);
    //    //}

    //    //LoadNext.transform.position += new Vector3(0, 0, this.transform.position.z + upsize / 2 * 100);
    //    //Debug.Log("位置已更新");
    //    //yield return new WaitForSeconds(0.3f);
    //}
}