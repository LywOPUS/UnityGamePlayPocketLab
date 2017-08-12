using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathData
{
    public int step = 0;
    public int[] pos;
    public int tag = 0;
    public int x;
    public int y;
    public bool isFind;

    public void Instance()
    {
    }

    public PathData(int y, int x)
    {
        this.y = y;
        this.x = x;
        isFind = false;
        this.pos = new int[] { y, x };
    }

    public GameObject path;
    public PathData pre;
}