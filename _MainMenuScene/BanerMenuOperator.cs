using UnityEngine;

public class BanerMenuOperator : MonoBehaviour
{
    public void Play()
    {
        MainMenuManager.SwitchPanels(MenuMarks.saves);
    }

    public void Options()
    {
        MainMenuManager.SwitchPanels(MenuMarks.options);
    }

    public void Credits()
    {
        MainMenuManager.SwitchPanels(MenuMarks.credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
