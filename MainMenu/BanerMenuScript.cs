using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BanerMenuScript : MonoBehaviour
{
    public void Continue()
    {
        LoadingSceneScript.LoadScene("WorldMap");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
