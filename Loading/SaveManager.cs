using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    static string path;

    void Awake()
    {
        path = Application.persistentDataPath;
    }

    public void Save(int account)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetPath(account), FileMode.Create);

        int Data = 1;

        formatter.Serialize(stream, Data);
        stream.Close();
    }

    public int Load(int account)
    {
        if(File.Exists(GetPath(account)))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int data = (int)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Save(account);
            return 0;
        }
        
    }

    void SaveFile()
    {

    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public bool FileExists(int i)
    {
        return FileExists(GetPath(i));
    }

    string GetPath(int i)
    {
        return path + $"data{i}.save";
    }
}
