using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveChoosingOperator : MonoBehaviour
{
    [SerializeField]
    private SaveManager saveManager;
    [SerializeField]
    private GameObject[] Jar;

    void Start()
    {
        if (saveManager == null)
        {
            saveManager = GameObject.FindGameObjectWithTag("AnyScene").GetComponent<SaveManager>();
        }

        for (int i = 0; i < 3; i++)
        {
            Jar[i].SetActive(saveManager.FileExists(i));
        }
    }

    public void Return()
    {
        MainMenuManager.SwitchPanels(MenuMarks.baner);
    }

    public void LoadSave(int i)
    {
        MainMenuManager.SwitchPanels(MenuMarks.off);
        SceneManager.LoadScene("WorldMap");
    }
}
