using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigUtil : MonoBehaviour
{
   
    public static ConfigUtil Instance;
    public Dictionary<string, DataConfig_Mineral> MineralConfig;

    public void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        MineralConfig = Load<DataConfig_Mineral>();
    }

#region 表导入导出
    /// <summary>
    /// 导入数据
    /// </summary>
    private Dictionary<string, T> Load<T>() where T : class
    {
        //寻找文本
        string rSheetName = typeof(T).Name;
        string readFilePath = Application.persistentDataPath + "/" + rSheetName + ".txt";

        string str;

        //读取文本
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
        //Unity自带的Json解析
        Dictionary<string, T> data = JsonUtility.FromJson<Dictionary<string, T>>(str);
        return data;
    }

    /// <summary>
    /// 导出数据
    /// </summary>
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
#endregion
}