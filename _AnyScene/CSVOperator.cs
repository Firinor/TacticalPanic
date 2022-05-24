using System.IO;
using UnityEngine;
using UnityEngine.Windows;
using Directory = UnityEngine.Windows.Directory;

public class CSVOperator : MonoBehaviour
{
    [SerializeField]
    private string path;

    void Start()
    {
        path = Application.dataPath + "/Database/Unitdata.csv";

        string AllText = SaveManager.Load<string>(path);

        Debug.Log(AllText);
    }
}
