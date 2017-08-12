using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathData
{
    public int step = 0;
    public int[] pos;
    public int tag = 0;

    public bool isFind;

    public PathData(int y, int x)
    {
        this.pos = new int[] { y, x };
    }

    public GameObject pathGO;
    public PathData pre;
}