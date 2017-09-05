using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigUtil : MonoBehaviour
{
    public static ConfigUtil Instance;

    public Dictionary<string, DataConfig_Bag> bagConfig;

    public void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        bagConfig = Load<DataConfig_Bag>();
        ExportToJson<DataConfig_Bag>(bagConfig);
    }

    /// <summary>
    /// 导入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private Dictionary<string, T> Load<T>() where T : class
    {
        string rSheetName = typeof(T).Name;
        string readFilePath = Application.persistentDataPath + "/" + rSheetName + ".txt";

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
            TextAsset textAsset = Resources.Load<TextAsset>("assetsbundles/data/sheet/" + rSheetName + "/" + rSheetName);
            if (textAsset == null)
            {
                Debug.LogError(rSheetName + "未找到");
                return null;
            }
            str = textAsset.text;
        }

        Dictionary<string, T> data = JsonUtility.FromJson<Dictionary<string, T>>(str);
        return data;
    }

    /// <summary>
    /// 导出数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rData"></param>
    public void ExportToJson<T>(Dictionary<string, T> rData) where T : class
    {
        string rSheetName = typeof(T).Name;

        string outFilePath = Application.persistentDataPath + "/" + rSheetName + ".txt";
        string jsonText = JsonUtility.ToJson(rData);

        Debug.Log(outFilePath);
        FileStream fs = new FileStream(outFilePath, FileMode.Create);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonText);

        fs.Write(data, 0, data.Length);

        fs.Flush();
        fs.Close();
    }
}