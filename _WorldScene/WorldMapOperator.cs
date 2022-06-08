using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapOperator : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToBattle(int level)
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void Options()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.options);
    }

    public void ToSquad()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.squad);
    }

    public void ToMap()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.map);
    }
}
