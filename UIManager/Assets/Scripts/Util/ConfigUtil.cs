using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;

public class configBagData
{
    public readonly string id;
    public readonly string itemName;
    public readonly string spriteName;
    public int count;
}

public class ConfigUtil : MonoBehaviour
{
    public static ConfigUtil Instance;

    public Dictionary<string, configBagData> bagConfig;

    public void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        Debug.Log("ConfigUtilInit\n\n");
        bagConfig = Load<configBagData>();

        ExportToJson<configBagData>(bagConfig);
        if (bagConfig != null)
        {
            Debug.Log("成功加载bagconfig");
        }
    }

    #region Jason流操作

    /// <summary>
    /// 导入数据
    /// </summary>
    private Dictionary<string, T> Load<T>() where T : class

    {
        Debug.Log(Application.persistentDataPath);
        string rSheeName = typeof(T).Name;

        string readFilePath = Application.persistentDataPath + "/" + rSheeName + ".txt";

        string str;

        if (File.Exists(readFilePath))
        {
            //Todo: 加载文件
            Debug.Log(rSheeName + "找到了");
            StreamReader textData = File.OpenText(readFilePath);
            str = textData.ReadToEnd();
            textData.Close();
            textData.Dispose();
        }
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>("assetsbundles/data/sheet/" + rSheeName);
            if (textAsset == null)
            {
                Debug.LogError(rSheeName + "未找到");
                return null;
            }
            str = textAsset.text;
        }

        Dictionary<string, T> data = JsonMapper.ToObject<Dictionary<string, T>>(str);
        Debug.Log(data["0000"].ToString());

        return data;
    }

    /// <summary>
    /// 导出数据到Json
    /// </summary>
    public void ExportToJson<T>(Dictionary<string, T> rData) where T : class
    {
        string rSheetName = typeof(T).Name;

        string outFilePath = Application.persistentDataPath + "/" + rSheetName + ".txt";

        string jsonText = JsonMapper.ToJson(rData);

        Debug.Log(outFilePath);
        FileStream fs = new FileStream(outFilePath, FileMode.Create);

        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonText);

        fs.Write(data, 0, data.Length);

        fs.Flush();
        fs.Close();
    }

    #endregion Jason流操作
}