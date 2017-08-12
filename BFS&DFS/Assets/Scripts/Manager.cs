using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    public int[] end;
    public GameObject floor;
    public int[] start;
    public GameObject wall;
    public GameObject test;
    private MapFile FMap;
    private PathData[,] PMap;

    private enum PATHDIR
    {
        up,
        down,
        left,
        right,
    }

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
                    PMap[j, i].step = 20;
                }
                else
                {
                    PMap[j, i].step = 0;
                }
            }
        }
    }

    /// <summary>
    /// 传入目标节点和方向，返回一个指定坐标的新节点
    /// </summary>
    /// <param name="path"></param>
    /// <param name="pathdir"></param>
    /// <returns></returns>
    //private PathData S_PMap(PathData path, PATHDIR pathdir)
    //{
    //    PathData temp = new PathData(path.pos[0], path.pos[1]);
    //    switch (pathdir)
    //    {
    //        case PATHDIR.up:
    //            if (temp.pos[0] < FMap.MapHight)
    //            {
    //                temp.pos[0] += 1;
    //            }
    //            else
    //            {
    //                return null;
    //            }

    //            break;

    //        case PATHDIR.down:
    //            if (temp.pos[0] > 0)
    //            {
    //                temp.pos[0] -= 1;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //            break;

    //        case PATHDIR.left:
    //            if (temp.pos[1] > 0)
    //            {
    //                temp.pos[1] -= 1;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //            break;

    //        case PATHDIR.right:
    //            if (temp.pos[1] < FMap.MapWidth)
    //            {
    //                temp.pos[1] += 1;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //            break;

    //        default:
    //            break;
    //    }

    //    return temp;
    //}

    /// <summary>
    /// 寻路任务
    /// </summary>
    /// <param name="path"></param>
    //private void Task(PathData path)
    //{
    //    path.isFind = true;
    //    if (path.isFind == false)
    //    {
    //        PathData up = S_PMap(path, PATHDIR.up);
    //        PathData down = S_PMap(path, PATHDIR.down);
    //        PathData left = S_PMap(path, PATHDIR.left);
    //        PathData right = S_PMap(path, PATHDIR.right);
    //    }
    //    else
    //    {
    //        Debug.Log("找过了");
    //    }
    //}

    public bool Canqueue(int[] pos)
    {
        if (pos[0] > 0 || pos[0] < FMap.MapHight - 1 || pos[1] > 0 || pos[1] < FMap.MapWidth - 1)
        {
            return true;
        }
        if (FMap.Map[pos[0], pos[1]] != 1 && PMap[pos[0], pos[1]].step < 1)
        {
            PMap[pos[0], pos[1]].step += 1;

            PMap[pos[0], pos[1]].path = Instantiate(test, new Vector2(pos[1], pos[0]), Quaternion.identity);
            return true;
        }

        return false;
    }

    private Queue<PathData> findqueue = new Queue<PathData>();

    public void Finding()
    {
        Queue<PathData[]> tampList = new Queue<PathData[]>();
        foreach (var item in findqueue)
        {
            if (item == PMap[18, 1])
            {
                Debug.Log("you finded");
                PathData currnt = item;

                Debug.Log(currnt.pos[0] + currnt.pos[1]);
                while (currnt.pre != null)
                {
                    currnt.path.GetComponent<Renderer>().material.color = Color.clear;
                    PMap[currnt.pos[0], currnt.pos[1]].path.GetComponent <>
                    currnt = currnt.pre;
                }
                break;
            }
            tampList.Enqueue(FindQueue(item.pos).ToArray());
        }
        findqueue.Dequeue();
        findqueue.Enqueue(tampList.ToArray());
    }

    public Queue<PathData> FindQueue(int[] pos)
    {
        Queue<PathData> tempqueue = new Queue<PathData>();
        if (Canqueue(new int[] { pos[0] + 1, pos[1] }))
        {
            tempqueue.Enqueue(PMap[pos[0] + 1, pos[1]]);
        }
        if (Canqueue(new int[] { pos[0] - 1, pos[1] }))
        {
            tempqueue.Enqueue(PMap[pos[0] - 1, pos[1]]);
        }
        if (Canqueue(new int[] { pos[0], pos[1] + 1 }))
        {
            tempqueue.Enqueue(PMap[pos[0], pos[1] + 1]);
        }
        if (Canqueue(new int[] { pos[0], pos[1] - 1 }))
        {
            tempqueue.Enqueue(PMap[pos[0], pos[1] - 1]);
        }
        return tempqueue;
    }

    //private PathData FindPath(PathData nextPath, PathData curruntPath)
    //{
    //    if (PMap[nextPath.pos[0], nextPath.pos[1]].step < 1 && PMap[nextPath.pos[0], nextPath.pos[1]].isFind == false)
    //    {
    //        Instantiate(test, new Vector2(nextPath.pos[1], nextPath.pos[0]), Quaternion.identity);

    //        return nextPath;
    //    }
    //    else return null;
    //}

    private Queue<List<PathData>> taskQue = new Queue<List<PathData>>();

    private void Start()
    {
        FMap = new MapFile(20, 20);
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
    }

    private void Update()
    {
    }
}