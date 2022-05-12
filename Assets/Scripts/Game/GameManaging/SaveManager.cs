using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SavePlayerStats(PlayerController playerStats)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string filePath = Application.persistentDataPath + "/player_stats.txt";
        FileStream fs = new FileStream(filePath, FileMode.Create);

        SaveData saveData = new SaveData(playerStats);

        bf.Serialize(fs, saveData);
        fs.Close();
    }
    public static SaveData LoadPlayerStats()
    {
        string filePath = Application.persistentDataPath + "/player_stats.txt";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath, FileMode.Open);

            SaveData saveData = bf.Deserialize(fs) as SaveData;
            fs.Close();
            return saveData;
        }
        else
        {
            return null;
        }
    }
}
