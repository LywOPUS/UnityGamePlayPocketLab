using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

internal class ReadMapFile
{
    private int MapWidth;
    private int MapHight;
    private int[] MapSize;
    public int[,] Map;

    public ReadMapFile(int width, int hight)
    {
        this.MapSize = new int[] { hight, width };
        this.Map = new int[MapSize[0], MapSize[1]];
    }

    public void NewMap(string fName)
    {
        string path = UnityEngine.Application.dataPath + "/Resources/" + fName;
        if (!File.Exists(path))
        {
            return;
        }
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader read = new StreamReader(fs, Encoding.Default);

        string mapLine = read.ReadLine();
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHight; j++)
            {
                Map[j, i] = 1;
                mapLine = read.ReadLine();
            }
        }
    }
}