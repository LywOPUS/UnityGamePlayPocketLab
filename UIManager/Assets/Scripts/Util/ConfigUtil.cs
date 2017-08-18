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
        bagConfig
    }

    Dictionary<string, T> load<T>() where T : class
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
        }
    }
}