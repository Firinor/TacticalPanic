using System.Text;
using UnityEngine;
using UnityEngine.UI;

public delegate void LogDelegat();

public class Log : MonoBehaviour
{
    public static event LogDelegat logEvent;

    private static StringBuilder mainLog = new StringBuilder();

    private static GameObject logPanel = GameObject.Find("GameLogsInfo");
    private static Text logText = logPanel.GetComponentInChildren<Text>();
    private static GameObject infoPanel = GameObject.Find("GameObjektsInfo");
    private static Image unitSprite = GameObject.Find("ObjectInfoImage").GetComponent<Image>();

    private void Awake()
    {
        logEvent += RefreshLogs;
    }

    public static void RefreshInfoPanel()
    {
        bool Pic = UnitController.SelectedUnits.Count > 0;
        logPanel.SetActive(!Pic);
        infoPanel.SetActive(Pic);
        if (Pic)
        {
            //GameObject.
            unitSprite.sprite = UnitController.SelectedUnits[0].GetComponent<Stats>().GetCardSprite();
        }
        else
        {
            RefreshLogs();
        }
    }

    public static void Info(StringBuilder log)
    {
        mainLog.AppendLine(log.ToString());
    }

    public static void RefreshLogs()
    {
        int length = Mathf.Min(mainLog.Length, 2000);
        logText.text = mainLog.ToString(mainLog.Length - length, length);
    }
}
