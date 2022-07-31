using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace TacticalPanicCode
{
    public enum SceneDirection { basic, exit, options, changeScene, saves, off }

    public class SceneManager : SinglBehaviour<SceneManager>
    {
        public static IScenePanel ScenePanel { get; set; }
        private AsyncOperation operation;

        [SerializeField]
        private GameObject optionsPanel;
        private OptionsOperator optionsOperator;
        [SerializeField]
        private LoadingTransitionOperator loadingTransitionOperator;
        [SerializeField]
        private GameObject[] doNotDestroyOnLoad;

        void Awake()
        {
            SingletoneCheck(this);

            optionsOperator = optionsPanel.GetComponent<OptionsOperator>();
            //optionsOperator is disabled. Awake & Start procedures are not suitable
            optionsOperator.SingletoneCheck(optionsOperator);//Singltone

            foreach (GameObject go in doNotDestroyOnLoad)
            {
                if (go != null)
                {
                    DontDestroyOnLoad(go);
                }
            }

            CheckingTheScene();
        }

        public static int GetScene()
        {
            return UnitySceneManager.GetActiveScene().buildIndex;
        }

        public static bool MenuScene()
        {
            return GetScene() == 0;
        }

        public static void LoadScene(string sceneName)
        {
            instance.operation = UnitySceneManager.LoadSceneAsync(sceneName);
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
                    DiactiveAllPanels();
                    ExitAction();
                    break;
                case SceneDirection.basic:
                    ScenePanel.BasicPanelSettings();
                    break;
                default:
                    throw new Exception("Unrealized bookmark!");
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
                throw new Exception("Error on exit button!");
            }
        }

        public static void DiactiveAllPanels()
        {
            instance.optionsPanel.SetActive(false);
        }

        public static void CheckingTheScene()
        {
            int SceneIndex = GetScene();
            if (SceneIndex == 2)//"BattleScene"
            {
                FindObjectOfType<BattleSceneManager>().SetAllInstance();
                instance.optionsOperator.SetAcriveOfExitButton(true);
            }
            else if (SceneIndex == 1)//"WorldMap"
            {
                FindObjectOfType<WorldMenuManager>().SetAllInstance();
                instance.optionsOperator.SetAcriveOfExitButton(true);
            }
            else if (SceneIndex == 0)//"MainMenu"
            {
                FindObjectOfType<MainMenuManager>().SetAllInstance();
                instance.optionsOperator.SetAcriveOfExitButton(false);
            }
            else
            {
                throw new Exception("Error on checking scene!");
            }
        }
    }
}