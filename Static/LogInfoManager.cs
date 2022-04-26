using UnityEngine;

public static class LogInfoManager
{
    private static GameObject logPanel = GameObject.Find("GameLogsInfo");
    private static GameObject infoPanel = GameObject.Find("GameObjektsInfo");

    public static void RefreshInfoPanel()
    {
        bool Pic = UnitController.SelectedUnits.Count > 0;

        logPanel.SetActive(!Pic);
        infoPanel.SetActive(Pic);
    }
}
