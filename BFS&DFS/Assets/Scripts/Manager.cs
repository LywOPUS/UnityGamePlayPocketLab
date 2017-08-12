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

    #region 路径地图信息相关代码

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

    /// <summary>
    /// 传入目标节点和方向，返回一个指定坐标的新节点
    /// </summary>
    /// <param name = "path" ></ param >
    /// < param name="pathdir"></param>
    /// <returns></returns>
    private bool CanSerch(PathData path, PATHDIR pathdir, ref PathData newPath)
    {
        if (path != null)
        {
            int y = path.pos[0];
            int x = path.pos[1];
            int up = y + 1;
            int down = y - 1;
            int left = x - 1;
            int right = x + 1;

            switch (pathdir)
            {
                case PATHDIR.up:
                    if (up < FMap.MapHight && PMap[up, x].step < 1 && FMap.Map[y, right] != 1)
                    {
                        PMap[up, x].step += 1;
                        PMap[up, x].pathGO = Instantiate(test, new Vector2(up, x), Quaternion.identity);
                        PMap[up, x].pre = path;
                        newPath = PMap[up, x];
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case PATHDIR.down:
                    if (down > 0 && PMap[down, x].step < 1 && FMap.Map[y, right] != 1)
                    {
                        PMap[down, x].step += 1;
                        PMap[down, x].pathGO = Instantiate(test, new Vector2(down, x), Quaternion.identity);
                        PMap[down, x].pre = path;
                        newPath = PMap[down, x];
                        return true;
                    }

                    return false;

                case PATHDIR.left:
                    if (left > 0 && PMap[y, left].step < 1 && FMap.Map[y, right] != 1)
                    {
                        PMap[y, left].step += 1;
                        PMap[y, left].pathGO = Instantiate(test, new Vector2(y, left), Quaternion.identity);
                        PMap[y, left].pre = path;
                        newPath = PMap[y, left];
                        return true;
                    }
                    return false;

                case PATHDIR.right:
                    if (right < FMap.MapWidth && PMap[y, right].step < 1 && FMap.Map[y, right] != 1)
                    {
                        PMap[y, right].step += 1;
                        PMap[y, right].pathGO = Instantiate(test, new Vector2(y, right), Quaternion.identity);
                        PMap[y, right].pre = path;
                        newPath = PMap[y, right];
                        return true;
                    }
                    return false;

                default:
                    return false;
            }
        }
        return false;
    }

    public bool Canqueue(int[] pos)
    {
        if ((pos[0] > 0 && pos[0] < FMap.MapHight - 1) && (pos[1] > 0 || pos[1] < FMap.MapWidth - 1))
        {
            return true;
        }
        if (FMap.Map[pos[0], pos[1]] != 1 && PMap[pos[0], pos[1]].step < 1)
        {
            PMap[pos[0], pos[1]].step += 1;
            PMap[pos[0], pos[1]].pathGO = Instantiate(test, new Vector2(pos[1], pos[0]), Quaternion.identity);
            return true;
        }

        return false;
    }

    public void FindQueue(int[] pos, Queue<PathData> temp)
    {
        if (Canqueue(new int[] { pos[0] + 1, pos[1] }))
        {
            temp.Enqueue(PMap[pos[0] + 1, pos[1]]);
        }
        if (Canqueue(new int[] { pos[0] - 1, pos[1] }))
        {
            temp.Enqueue(PMap[pos[0] - 1, pos[1]]);
        }
        if (Canqueue(new int[] { pos[0], pos[1] + 1 }))
        {
            temp.Enqueue(PMap[pos[0], pos[1] + 1]);
        }
        if (Canqueue(new int[] { pos[0], pos[1] - 1 }))
        {
            temp.Enqueue(PMap[pos[0], pos[1] - 1]);
        }
    }

    private Queue<PathData> FindPath = new Queue<PathData>();

    public IEnumerator Finding()
    {
        PathData temp = new PathData(0, 0);
        FindPath.Enqueue(PMap[1, 1]);
        PMap[1, 1].step = 1;
        while (FindPath.Count > 0)
        {
            PathData currnt = FindPath.Dequeue();
            if (currnt == PMap[18, 1])
            {
                Debug.Log("YOU FIND!!!!!!!!");
                while (currnt.pre != null)
                {
                    currnt.pathGO.GetComponent<Renderer>().material.color = Color.clear;
                    PMap[currnt.pos[0], currnt.pos[1]].pathGO.GetComponent<Renderer>().material.color = Color.yellow;
                    currnt = currnt.pre;
                }
                break;
            }
            yield return 0;
            FindQueue(currnt.pos, FindPath);
        }

        Debug.Log("you finded");
        //PathData currnt =

        //Debug.Log(currnt.pos[0] + currnt.pos[1]);
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
        StartCoroutine(Finding());
    }
}