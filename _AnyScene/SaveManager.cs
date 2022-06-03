using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    static string path;

    void Awake()
    {
        //C:\Users\<userprofile>\AppData\LocalLow\<companyname>\<productname>
        path = Application.persistentDataPath;
        OptionsOperator.LoadOptions();
    }

    public static void Save<T>(string path, T data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void CreateNewSave(int account)
    {
        Save(GetPath(account), account);
    }

    public static void Save(int account)
    {
        Save(GetPath(account), account);
    }


    public static void SaveOptions()
    {
        Save<OptionsParameters>(GetOptionPath(), OptionsOperator.GetParameters());
    }

    public static OptionsParameters LoadOptions()
    {
        return Load<OptionsParameters>(GetOptionPath());
    }

    public static void Load(int account)
    {
        //Load(GetPath(account));
    }

    public static T Load<T>(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            T data = (T)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            return default;
        }
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public bool FileExists(int i)
    {
        return FileExists(GetPath(i));
    }

    static string GetPath(int i)
    {
        return path + $"data{i}.save";
    }

    static string GetOptionPath()
    {
        return path + $"option.save";
    }
}
