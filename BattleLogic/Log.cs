using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    private static GameObject infoPanel = GameObject.Find("GameObjektsInfo");
    private static Image unitSprite = GameObject.Find("ObjectInfoImage").GetComponent<Image>();
    private static Text infoText = infoPanel.GetComponentInChildren<Text>();

    private static GameObject unitFromThePoster;

    public static void RefreshInfoPanel()
    {
        bool Pic = UnitController.SelectedUnits.Count > 0;
        if (Pic)
        {
            if(unitFromThePoster == UnitController.SelectedUnits[0])
            {
                return;
            }
            unitFromThePoster = UnitController.SelectedUnits[0];
            //GameObject.
            unitSprite.sprite = unitFromThePoster.GetComponent<Stats>().GetCardSprite();
        }
    }
}
