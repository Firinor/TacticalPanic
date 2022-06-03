using System;
using UnityEngine;

public enum WorldMarks { options, squad, magic, blacksmith, off }

public class WorldMenuManager : MonoBehaviour, IGScenePanel
{

    [SerializeField]
    private GameObject squad;
    [SerializeField]
    private GameObject magic;
    [SerializeField]
    private GameObject blacksmith;
    public static WorldMenuManager instance { get; private set; }

    public void SetAllInstance()
    {
        instance = this;
        SceneManager.scenePanel = this;

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
            case WorldMarks.off:
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
        //squad.SetActive(false);
        //magic.SetActive(false);
        //blacksmith.SetActive(false);
        SceneManager.DiactiveAllPanels();
    }

    public void BasicPanelSettings()
    {
        SwitchPanels(WorldMarks.off);
    }
}
