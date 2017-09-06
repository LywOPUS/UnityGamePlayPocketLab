using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralData
{
    //ToDo：图鉴表存储读取
    private static MineralData instance;

    public static MineralData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MineralData();
                instance.MineralMapDict = ConfigUtil.Instance.MineralConfig;
            }

            return instance;
        }
    }

    public Dictionary<string, DataConfig_Mineral> MineralMapDict = new Dictionary<string, DataConfig_Mineral>();
}