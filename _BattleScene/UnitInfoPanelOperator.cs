using UnityEngine;
using UnityEngine.UI;

public delegate void UnitInfoDelegate();

public static class UnitInfoPanelOperator
{
    public static event UnitInfoDelegate InfoEvent;

    private static GameObject infoPanel = GameObject.Find("HitBarsInfoPanel");
    private static Image unitSprite = GameObject.Find("ObjectInfoImage").GetComponent<Image>();
    private static Text infoText = GameObject.Find("ContentTextInfoPanel").GetComponentInChildren<Text>();

    private static Slider[] InfoSliderBar = infoPanel.GetComponentsInChildren<Slider>();
    private static Text[] InfoTextBar = infoPanel.GetComponentsInChildren<Text>();

    private static GameObject unitFromThePoster;

    public static void RefreshInfoPanel()
    {
        bool Pic = SelectedUnitsInformator.SelectedUnits.Count > 0;
        if (Pic)
        {
            if(unitFromThePoster == SelectedUnitsInformator.SelectedUnits[0])
            {
                return;
            }
            if(unitFromThePoster != null)
                unitFromThePoster.GetComponent<Unit>().UnPick();
            unitFromThePoster = SelectedUnitsInformator.SelectedUnits[0];
            unitFromThePoster.GetComponent<Unit>().Pick();
            unitSprite.sprite = unitFromThePoster.GetComponent<Unit>().GetCardSprite();
        }
        RefreshPointsInfo();
    }

    public static void RefreshPointsInfo(GameObject unit)
    {
        if (unit == unitFromThePoster)
        {
            RefreshPointsInfo();
        }
    }
    public static void RefreshPointsInfo()
    {
        if(unitFromThePoster != null)
        {
            Unit.PointsValue[] pointInfo = unitFromThePoster.GetComponent<Unit>().GetPointInfo();

            for (int i = 0; i < pointInfo.Length; i++)
            {
                if (pointInfo[i].max > 0)
                {
                    InfoSliderBar[i].maxValue = pointInfo[i].max;
                    InfoSliderBar[i].value = pointInfo[i].current;
                    InfoTextBar[i].text = $"{(int)pointInfo[i].current}/{pointInfo[i].max}";
                }
                else
                {

                }
            }
        }
    }

    public static void ClearInfoPanel()
    {
        unitSprite.sprite = null;
    }
}
