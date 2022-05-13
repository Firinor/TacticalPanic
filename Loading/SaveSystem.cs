using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    static string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/data.save";
    }

    public void SaveLevel()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        int Data = 1;

        formatter.Serialize(stream, Data);
        stream.Close();
    }

    public int LoadLevel()
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            new UnityException("There is no one save file");
            return 0;
        }
        
    }

    void SaveFile()
    {

    }
}
