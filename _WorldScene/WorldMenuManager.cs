using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldMarks { options, squad, magic, blacksmith, off }

public class WorldMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject squad;
    [SerializeField]
    private GameObject magic;
    [SerializeField]
    private GameObject blacksmith;
    private GameObject options;
    public static WorldMenuManager instance { get; private set; }

    void Awake()
    {
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<WorldMenuManager>();
        options = Resources.FindObjectsOfTypeAll<OptionsOperator>()[0].gameObject;
    }

    public void SwitchMenuMarks(WorldMarks mark)
    {
        DiactiveAllMenuMarks();
        switch (mark)
        {
            case WorldMarks.squad:
                squad.SetActive(true);
                break;
            case WorldMarks.magic:
                magic.SetActive(true);
                break;
            case WorldMarks.options:
                options.SetActive(true);
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
    public void SwitchMenuMarks(int mark)
    {
        SwitchMenuMarks((WorldMarks)mark);
    }

    private void DiactiveAllMenuMarks()
    {
        squad?.SetActive(false);
        magic?.SetActive(false);
        blacksmith?.SetActive(false);
        options?.SetActive(false);
    }
}
