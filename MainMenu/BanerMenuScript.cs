using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BanerMenuScript : MonoBehaviour
{
    public void Play()
    {
        MainMenuManager.instance.SwitchMenuMarks(MenuMarks.saves);
        //LoadingSceneScript.LoadScene("WorldMap");
    }

    public void Options()
    {
        MainMenuManager.instance.SwitchMenuMarks(MenuMarks.options);
    }

    public void Credits()
    {
        MainMenuManager.instance.SwitchMenuMarks(MenuMarks.credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
