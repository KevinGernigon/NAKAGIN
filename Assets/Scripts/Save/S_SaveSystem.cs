using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using  System.Runtime.Serialization.Formatters.Binary;


public static class S_SaveSystem
{
    public static void SavePlayer(S_SaveData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        S_PlayerData data = new S_PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static S_PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            S_PlayerData data = formatter.Deserialize(stream) as S_PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in "+ path);
            return null;
        }
    }
}
