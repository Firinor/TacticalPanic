using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleMarks { options, off }

public class BattleSceneManager : SinglBehaviour<WorldMenuManager>, IScenePanel
{
    [SerializeField]
    private GameObject unitsTank;
    private UnitsManager unitsManager;

    public void SetAllInstance()
    {
        if (instance == null)
            SingletoneCheck(this);
        SceneManager.ScenePanel = this;

        unitsManager = unitsTank.GetComponent<UnitsManager>();
        unitsManager.SingletoneCheck(unitsManager);//Singltone
    }

    public void SwitchPanels(BattleMarks mark)
    {
        DiactiveAllPanels();
        switch (mark)
        {
            case BattleMarks.off:
                break;
            case BattleMarks.options:
                SceneManager.SwitchPanels(SceneDirection.options);
                break;
            default:
                new Exception("Unrealized bookmark!");
                break;
        }
    }

    public void SwitchPanels(int mark)
    {
        SwitchPanels((BattleMarks)mark);
    }

    public void DiactiveAllPanels()
    {
        SceneManager.DiactiveAllPanels();
    }

    public void BasicPanelSettings()
    {
        SwitchPanels(BattleMarks.off);
    }
}
