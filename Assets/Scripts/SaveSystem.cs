using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    static readonly string saveFilePath = Application.persistentDataPath + "/PlayerData/SaveData.XEN";

    public static void SaveData(PlayerData data)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/PlayerData"))
            Directory.CreateDirectory(Application.persistentDataPath + "/PlayerData");

        File.WriteAllText(saveFilePath, JsonUtility.ToJson(data, true));
        //Debug.Log($"PlayerData Saved to {saveFilePath}");
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            //Debug.Log($"Loading Data from {saveFilePath}");
            return JsonUtility.FromJson<PlayerData>(File.ReadAllText(saveFilePath));
        }
        return new PlayerData();
    }
}
