using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

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
        //Todo:没解析成功
        MineralConfig = Load<DataConfig_Mineral>();
        Debug.Log(MineralConfig["001"].name);
    }

    #region 表导入导出

    /// <summary>
    /// json解析
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
            TextAsset textAsset = Resources.Load<TextAsset>("assetsbundles/data/sheet/" + rSheetName);
            if (textAsset == null)
            {
                Debug.LogError(rSheetName + "未找到");
                return null;
            }
            str = textAsset.text;
        }

        //Json解析
        //Unity自带的Json解析和UnityEngine在同一个命名空间，所以可以直接使用
        //但是jsonutility不能解析字典
        Debug.Log(str);
        var temp = JsonUtility.FromJson<T>(str);
        // Dictionary<string, T> data = JsonUtility.FromJson<Dictionary<string, T>>(str);
        Dictionary<string, T> data = JsonMapper.ToObject<Dictionary<string, T>>(str);

        return data;
    }

    /// <summary>
    /// 导出到Json
    /// </summary>
    public void ExportToJson<T>(Dictionary<string, T> rData) where T : class
    {
        string rSheetName = typeof(T).Name;

        string outFilePath = Application.persistentDataPath + "/" + rSheetName + ".txt";

        //string jsonText = JsonUtility.ToJson(rData);
        string jsonText = JsonMapper.ToJson(rData);
        Debug.Log(outFilePath);
        FileStream fs = new FileStream(outFilePath, FileMode.Create);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonText);

        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
    }

    #endregion 表导入导出
}