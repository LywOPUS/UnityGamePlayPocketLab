using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WallPwan : MonoBehaviour
{
    private int[,] map = new int[200, 200];
    public GameObject wall;

    public struct PathPos
    {
        private int xPos;
        private int yPos;
    }

    private void Start()
    {
        ReadMapFile();
        StartCoroutine(InstanceWall());
    }

    private void Update()
    {
    }

    public void ReadMapFile()
    {
        #region 读取文件

        string path = Application.dataPath + "/Resources/" + "map.txt";
        if (!File.Exists(path))
        {
            return;
        }
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader read = new StreamReader(fs, Encoding.Default);

        #endregion 读取文件

        string Wallfile = read.ReadLine();
        int y = 0;
        while (Wallfile != null)
        {
            for (int x = 0; x < Wallfile.Length; x++)
            {
                if (Wallfile[x].Equals('#'))
                {
                    map[y, x] = 1;
                }
                if (Wallfile[x].Equals('-'))
                {
                    map[y, x] = 2;
                }
                if (Wallfile[x].Equals('+'))
                {
                    map[y, x] = 3;
                }
            }
            y++;
            Wallfile = read.ReadLine();
        }
    }

    public IEnumerator InstanceWall()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[y, x] == 1)
                {
                    Instantiate(wall, new Vector3(x, 0, -y), Quaternion.identity);
                    yield return 0;
                }
            }
        }
        yield return null;
    }
}