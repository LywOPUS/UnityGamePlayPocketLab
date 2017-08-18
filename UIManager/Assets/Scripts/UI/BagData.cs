using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagData : MonoBehaviour
{
    private static BagData instance;
    public static BagData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BagData();
                instance.curItemDict = ConfigUtil.Instance.bagConfig;
            }
            return instance;
        }
    }
    public Dictionary<string, configBagData> curItemDict = new Dictionary<string, configBagData>();
}