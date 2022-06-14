using System;
using UnityEngine;

public enum WorldMarks { options, squad, magic, blacksmith, map }

public class WorldMenuManager : SinglBehaviour<WorldMenuManager>, IScenePanel
{

    [SerializeField]
    private GameObject levels;
    [SerializeField]
    private GameObject squad;
    [SerializeField]
    private GameObject magic;
    [SerializeField]
    private GameObject blacksmith;

    public void SetAllInstance()
    {
        if(instance == null)
            SingletoneCheck(this);
        SceneManager.ScenePanel = this;
    }

    public void SwitchPanels(WorldMarks mark)
    {
        DiactiveAllPanels();
        switch (mark)
        {
            case WorldMarks.squad:
                squad.SetActive(true);
                break;
            case WorldMarks.magic:
                magic.SetActive(true);
                break;
            case WorldMarks.options:
                SceneManager.SwitchPanels(SceneDirection.options);
                break;
            case WorldMarks.blacksmith:
                blacksmith.SetActive(true);
                break;
            case WorldMarks.map:
                levels.SetActive(true);
                break;
            default:
                new Exception("Unrealized bookmark!");
                break;
        }
    }

    public void SwitchPanels(int mark)
    {
        SwitchPanels((WorldMarks)mark);
    }

    public void DiactiveAllPanels()
    {
        squad.SetActive(false);
        levels.SetActive(false);
        //magic.SetActive(false);
        //blacksmith.SetActive(false);
        SceneManager.DiactiveAllPanels();
    }

    public void BasicPanelSettings()
    {
        SwitchPanels(WorldMarks.map);
    }
}
