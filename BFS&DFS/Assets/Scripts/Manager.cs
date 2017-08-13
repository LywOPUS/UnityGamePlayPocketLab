using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    public int[] end;
    public GameObject floor;
    public GameObject test;
    public GameObject wall;
    public GameObject wallParent;
    private Queue<PathData> FindPath = new Queue<PathData>();
    private MapFile FMap;
    private PathData[,] PMap;

    public Transform[] targetPoint;

    #region 路径地图信息相关代码

    //生成搜寻地图信息
    private void New_PMap()
    {
        for (int i = 0; i < FMap.MapWidth; i++)
        {
            for (int j = 0; j < FMap.MapHight; j++)
            {
                PMap[j, i] = new PathData(j, i);
                Debug.Log(PMap[j, i]);
                if (FMap.Map[j, i] == 1)
                {
                    PMap[j, i].step = 10000;
                }
                else
                {
                    PMap[j, i].step = 0;
                }
            }
        }
    }

    #endregion 路径地图信息相关代码

    public bool Canqueue(int[] pos)
    {
        if ((pos[0] > 0 && pos[0] < FMap.MapHight - 1) && (pos[1] > 0 || pos[1] < FMap.MapWidth - 1))
        {
            if (FMap.Map[pos[0], pos[1]] != 1 && PMap[pos[0], pos[1]].step < 1)
            {
                PMap[pos[0], pos[1]].step += 1;

                PMap[pos[0], pos[1]].pathGO = Instantiate(test, new Vector2(pos[1], pos[0]), Quaternion.identity);
                return true;
            }
        }
        return false;
    }

    public void FindQueue(int[] pos, Queue<PathData> temp)
    {
        if (Canqueue(new int[] { pos[0] + 1, pos[1] }))
        {
            PMap[pos[0] + 1, pos[1]].pre = PMap[pos[0], pos[1]];
            temp.Enqueue(PMap[pos[0] + 1, pos[1]]);
        }
        if (Canqueue(new int[] { pos[0] - 1, pos[1] }))
        {
            PMap[pos[0] - 1, pos[1]].pre = PMap[pos[0], pos[1]];
            temp.Enqueue(PMap[pos[0] - 1, pos[1]]);
        }
        if (Canqueue(new int[] { pos[0], pos[1] + 1 }))
        {
            PMap[pos[0], pos[1] + 1].pre = PMap[pos[0], pos[1]];
            temp.Enqueue(PMap[pos[0], pos[1] + 1]);
        }
        if (Canqueue(new int[] { pos[0], pos[1] - 1 }))
        {
            PMap[pos[0], pos[1] - 1].pre = PMap[pos[0], pos[1]];
            temp.Enqueue(PMap[pos[0], pos[1] - 1]);
        }
    }

    public IEnumerator Finding()
    {
        FindPath.Enqueue(PMap[(int)targetPoint[0].transform.position.y, (int)targetPoint[0].transform.position.x]);
        PMap[(int)targetPoint[0].transform.position.y, (int)targetPoint[0].transform.position.x].pre = null;
        PMap[1, 1].step = 1;
        while (FindPath.Count > 0)
        {
            //Debug.Log(FindPath.Peek().pos[0] + FindPath.Peek().pos[1]);
            PathData currnt = FindPath.Dequeue();
            if (currnt == PMap[(int)targetPoint[1].transform.position.y, (int)targetPoint[1].transform.position.x])
            {
                Debug.Log("YOU FIND!!!!!!!!");
                while (PMap[currnt.pos[0], currnt.pos[1]].pre != null)
                {
                    currnt.pathGO.GetComponent<Renderer>().material.color = Color.yellow;
                    Debug.Log(PMap[currnt.pos[0], currnt.pos[1]].step);
                    PMap[currnt.pos[0], currnt.pos[1]].pathGO.GetComponent<SpriteRenderer>().color = Color.yellow;
                    currnt = currnt.pre;
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            }
            yield return 0;
            FindQueue(currnt.pos, FindPath);
        }
    }

    private void Start()
    {
        FMap = new MapFile(20, 20);
        FMap.mapParent = wallParent;
        FMap.NewMap("map.txt", wall);
        PMap = new PathData[FMap.MapHight, FMap.MapWidth];
        New_PMap();
        PMap[1, 1].tag = -1;
        PMap[18, 1].tag = 1;

        foreach (var item in PMap)
        {
            if (item.step < 1)
            {
                Instantiate(floor, new Vector2(item.pos[1], item.pos[0]), Quaternion.identity);
            }
        }
        StartCoroutine(Finding());
    }
}