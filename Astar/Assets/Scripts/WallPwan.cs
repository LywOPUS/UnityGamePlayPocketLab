using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Node
{
    public Node parent;
    public int[] pos;
    public int step;
    private int x;
    private int y;
    public bool isFind;
    public bool isEnd;
    public bool isWall;

    public Node(int x, int y)
    {
        this.pos = new int[] { y, x };
        this.isFind = false;
    }
}

public class WallPwan : MonoBehaviour
{
    public GameObject wall;
    public GameObject PathQube;
    private static int[,] map = new int[200, 200];

    private void Start()
    {
        ReadMapFile();
        Debug.Log("开始了");
        StartCoroutine(InstanceWall());
        NodeMpath();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFind();
        }
    }

    #region BPF

    public List<Node> findList = new List<Node>();
    public Node[,] nodes = new Node[map.GetLength(0), map.GetLength(1)];
    public GameObject[] wayPoint = new GameObject[3];

    private void StartFind()
    {
        List<Node> temp = new List<Node>();
        findList.Add(nodes[-(int)wayPoint[0].transform.position.z, (int)wayPoint[0].transform.position.x]);
        Debug.Log("is Start");
        foreach (var item in findList)
        {
            if (item == nodes[-(int)wayPoint[1].transform.position.z, (int)wayPoint[1].transform.position.x])
            {
                Debug.Log("你找到了");

                // return false;
            }
            Debug.Log("你没找到");
            temp.AddRange(FindPath(item.pos));
        }
        findList.Clear();
        findList = temp;
        //return true;
    }

    private void NodeMpath()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                nodes[j, i] = new Node(j, i);

                //墙
                if (map[j, i] == 1)
                {
                    nodes[j, i].isWall = true;
                }
            }
        }
        //nodes[(int)wayPoint[1].transform.position.z, (int)wayPoint[1].transform.position.x].isEnd = true;
    }

    public List<Node> FindPath(int[] Pos)
    {
        Debug.Log("开始找路了");
        List<Node> tempList = new List<Node>();
        if (CanFind(new int[] { Pos[0] + 1, Pos[1] }))
        {
            Debug.Log("超上找");
            tempList.Add(nodes[Pos[0] + 1, Pos[1]]);

            nodes[Pos[0] + 1, Pos[1]].parent = nodes[Pos[0], Pos[1]];
        }
        if (CanFind(new int[] { Pos[0] - 1, Pos[1] }))
        {
            Debug.Log("朝下找");
            tempList.Add(nodes[Pos[0] - 1, Pos[1]]);
            //  nodes[Pos[0], Pos[1]].step += 1;
            nodes[Pos[0] - 1, Pos[1]].parent = nodes[Pos[0], Pos[1]];
        }
        if (CanFind(new int[] { Pos[0], Pos[1] + 1 }))
        {
            Debug.Log("朝左找");
            tempList.Add(nodes[Pos[0], Pos[1] + 1]);
            //nodes[Pos[0], Pos[1]+1].step += 1;
            nodes[Pos[0], Pos[1] + 1].parent = nodes[Pos[0], Pos[1]];
        }
        if (CanFind(new int[] { Pos[0], Pos[1] - 1 }))
        {
            Debug.Log("朝右边找");
            tempList.Add(nodes[Pos[0], Pos[1] - 1]);
            //nodes[Pos[0], Pos[1]].step += 1;
            nodes[Pos[0], Pos[1] - 1].parent = nodes[Pos[0], Pos[1]];
        }

        return tempList;
    }

    public bool CanFind(int[] pos)
    {
        Debug.Log("进入Canfind");
        //if (nodes[pos[0], pos[1]].pos >)
        //{
        //    return false;
        //}
        //if (pos[0] < map.GetLength(0) && pos[1] < map.GetLength(1))
        //{
        //if (nodes[pos[0], pos[1]].isEnd != true)
        //{
        //    // Debug.Log("You Find Me");

        //    return false;
        //}
        if (nodes[pos[0], pos[1]].isFind == true)
        {
            Debug.Log("这里找过了");
            //Instantiate(PathQube, new Vector3(pos[0], 0, -pos[1]), Quaternion.identity);
            return false;
        }
        if (nodes[pos[0], pos[1]].isWall)
        {
            Debug.Log("这里是墙");
            //Instantiate(PathQube, new Vector3(pos[0], 0, -pos[1]), Quaternion.identity);
            return false;
        }
        else
        {
            Debug.Log("生成了路");
            nodes[pos[0], pos[1]].isFind = true;
            Instantiate(PathQube, new Vector3(pos[0], 0, -pos[1]), Quaternion.identity);
            return true;
        }
    }

    #endregion BPF

    #region CreatMap

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
            }
            y++;
            Wallfile = read.ReadLine();
        }
    }

    #endregion CreatMap
}