using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagData
{
    public Dictionary<string, configBagData> curItemDict;
    private static BagData instance;

    public static BagData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BagData
                {
                    curItemDict = ConfigUtil.Instance.bagConfig
                };
            }
            return instance;
        }
    }
}