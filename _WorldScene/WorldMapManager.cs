using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
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
        WorldMenuManager.instance.SwitchMenuMarks(WorldMarks.options);
    }
}
