using UnityEngine;
using System.IO;
using Newtonsoft.Json;
[DefaultExecutionOrder(-1)]
public static class GameLS
{
    public static t LoadDataFromJson<t>(string JsonString) where t : new()
    {
        t DynamicData = JsonUtility.FromJson<t>(JsonString);
        if (DynamicData == null)
        {
            Debug.Log(typeof(t).ToString() + "is null");
            DynamicData = new t();
        }
        return DynamicData;
    }
    public static void SaveGameby(string filename, DynamicData dynamicData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        string jsonString = JsonUtility.ToJson(dynamicData);
        File.WriteAllText(filePath, jsonString);
        Debug.Log(dynamicData.ToString() + " saved to: " + filePath);
    }
    public static string SaveGameby<t>(t dynamicData) where t : DynamicData
    {
        return JsonUtility.ToJson(dynamicData);
    }
    public static string SaveGameConvertby<t>(t dynamicData) where t : DynamicData
    {
        return JsonConvert.SerializeObject(dynamicData);
    }

    public static bool LoadDataFromJson<t>(string JsonString, t originData, out t outdata)
    {
        t DynamicData = JsonUtility.FromJson<t>(JsonString);
        if (DynamicData == null)
        {
            Debug.Log(typeof(t).ToString() + "is null");
            DynamicData = originData;
        }
        outdata = DynamicData;
        return true;
    }
    public static bool LoadDataFromJsonConvert<t>(string JsonString, t originData, out t outdata)
    {
        t DynamicData = JsonConvert.DeserializeObject<t>(JsonString);
        if (DynamicData == null || JsonString == "")
        {
            Debug.Log(typeof(t).ToString() + "is null");
            DynamicData = originData;
        }
        outdata = DynamicData;
        return true;
    }
    
}
