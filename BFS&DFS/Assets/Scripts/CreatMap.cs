using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

internal class MapFile
{
    public int MapWidth;
    public int MapHight;
    private int[] MapSize;
    public int[,] Map;

    public MapFile(int width, int hight)
    {
        this.MapHight = hight;
        this.MapWidth = width;
        this.MapSize = new int[] { MapHight, MapWidth };
        this.Map = new int[MapSize[0], MapSize[1]];
    }

    public void NewMap(string fName, GameObject wall)
    {
        // Debug.Log("newmap" + " " + MapWidth + " " + MapHight + " " + Map[0, 0]);
        string path = UnityEngine.Application.dataPath + "/Resources/" + fName;
        if (!File.Exists(path))
        {
            Debug.Log("return");
            return;
        }
        // Debug.Log("startFileread");
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader read = new StreamReader(fs, Encoding.Default);

        string mapLine = read.ReadLine();
        //Debug.Log(mapLine);
        int j = 0;
        while (mapLine != null)
        {
            for (int i = 0; i < MapWidth; i++)
            {
                if (mapLine[i] == '1')
                {
                    Map[j, i] = 1;

                    UnityEngine.Object.Instantiate(wall, new Vector2(i, j), Quaternion.identity);
                }
            }

            j++;
            mapLine = read.ReadLine();
        }
    }
}