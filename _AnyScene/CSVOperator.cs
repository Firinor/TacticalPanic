using System.IO;
using UnityEngine;

public class CSVOperator : MonoBehaviour
{
    private string path;

    void Start()
    {
        path = Application.dataPath + "/Database/UnitData.csv";

        string[] AllText = File.ReadAllLines(path);

        foreach (string text in AllText)
            Debug.Log(text);
    }
}
