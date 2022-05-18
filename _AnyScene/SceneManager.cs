using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManagement = UnityEngine.SceneManagement.SceneManager;

public enum SceneDirection { basic, exit, options, changeScene, saves, off }

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;
    private Scene currentScene;
    public static IScenePanel scenePanel { get; set; }
    private AsyncOperation operation;

    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private LoadingTransitionOperator loadingTransitionOperator;
    [SerializeField]
    private GameObject[] doNotDestroyOnLoad;

    void Start()
    {
        instance = this;
        currentScene = UnitySceneManagement.GetActiveScene();

        foreach (GameObject go in doNotDestroyOnLoad)
        {
            if (go != null)
            {
                DontDestroyOnLoad(go);
            }
        }
    }

    public static SceneManager GetSceneManager()
    {
        return instance;
    }

    public static int GetScene()
    {
        return instance.currentScene.buildIndex;
    }

    public static bool MenuScene()
    {
        return instance.currentScene.buildIndex == 0;
    }

    public static void LoadScene(string sceneName, int data = 0)
    {
        instance.operation = UnitySceneManagement.LoadSceneAsync(sceneName);
        SetAllowSceneActivation(false);
        instance.loadingTransitionOperator.LoadScene();
    }

    public static void SetAllowSceneActivation(bool b)
    {
        instance.operation.allowSceneActivation = b;
    }

    public static void SwitchPanels(SceneDirection direction)
    {
        switch (direction)
        {
            case SceneDirection.options:
                instance.optionsPanel.SetActive(true);
                break;
            case SceneDirection.exit:
                ExitAction();
                break;
            case SceneDirection.basic:
                scenePanel.BasicPanelSettings();
                break;
            default:
                new Exception("Unrealized bookmark!");
                break;
        }
    }

    private static void ExitAction()
    {
        int SceneIndex = GetScene();
        if (SceneIndex == 2)//"BattleScene"
        {
            LoadScene("WorldMap");
        }
        else if (SceneIndex == 1)//"WorldMap"
        {
            LoadScene("MainMenu");
        }
        else if (SceneIndex == 0)//"MainMenu"
        {
            Application.Quit();
        }
        else
        {
            new Exception("Error on exit button!");
        }
    }

    public static void DiactiveAllPanels()
    {
        instance.optionsPanel.SetActive(false);
    }
}