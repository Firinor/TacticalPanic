using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapOperator : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToBriefing(int levelCode)
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.briefing);
        UnitsCardManager.CardsToParent(CardHolder.BriefingCanvas);
        BriefingCanvasOperator.SetLevelInfo(DB.ReadLevel(levelCode));
    }

    public void ToBattle()
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

    public void ToHome()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.home);
    }

    public void ToMagic()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.magic);
    }

    public void ToBlacksmith()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.blacksmith);
    }

    public void ToSocial()
    {
        WorldMenuManager.instance.SwitchPanels(WorldMarks.social);
    }
}
