using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapOperator : MonoBehaviour
{

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToBriefing(int level)
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.briefing);
        UnitsCardManager.CardsToParent(CardHolder.BriefingCanvas);
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
        UnitsCardManager.CardsToParent(CardHolder.SquadCanvas);

    }

    public void ToMap()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.map);
    }
}
