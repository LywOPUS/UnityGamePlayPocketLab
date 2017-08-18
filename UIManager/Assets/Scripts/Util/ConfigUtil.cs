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
        bagConfig = Load<configBagData>();
    }

    /// <summary>
    /// 导入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private Dictionary<string, T> Load<T>() where T : class
    {
        string rSheeName = typeof(T).Name;

        string readFilePath = Application.persistentDataPath + "/" + rSheeName + ".txt";

        string str;

        if (File.Exists(readFilePath))
        {
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

        return data;
    }

    public void ExportToJson<T>(Dictionary<string, T> rData) where T : class
    {
        string rSheetName = typeof(T).Name;

        string outFilePath = Application.persistentDataPath + "/data/" + rSheetName + ".txt";

        string jsonText = JsonMapper.ToJson(rData);

        Debug.Log(outFilePath);
        FileStream fs = new FileStream(outFilePath, FileMode.Create);

        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonText);

        fs.Write(data, 0, data.Length);

        fs.Flush();
        fs.Close();
    }
}