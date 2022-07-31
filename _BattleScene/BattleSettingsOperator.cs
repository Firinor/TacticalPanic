using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class BattleSettingsOperator : MonoBehaviour
    {
        public static void SetGameSpeed(float newSpeed)
        {
            TimeManager.SetGameSpeed(newSpeed);
        }

        public void Options()
        {
            SceneManager.SwitchPanels(SceneDirection.options);
        }
    }
}
