using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class CreditChallengeOperator : MonoBehaviour
    {
        public void Return()
        {
            MainMenuManager.SwitchPanels(MenuMarks.baner);
        }
    }
}
