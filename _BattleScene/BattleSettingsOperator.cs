using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSettingsOperator : MonoBehaviour
{
    public static void SetGameSpeed(float newSpeed)
    {
        TimeManager.SetGameSpeed(newSpeed);
    }

    public void Options()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.options);
    }
}