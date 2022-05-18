using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsOperator : MonoBehaviour
{
    [SerializeField]
    private GameObject exitButton;
    [SerializeField]
    private SceneManager sceneManager;

    void Awake()
    {
        if(sceneManager == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("AnyScene");
            sceneManager = go.GetComponent<SceneManager>();
        }
    }

    public void Return()
    {
        SceneManager.SwitchPanels(SceneDirection.basic);
    }
    public void Exit()
    {
        SceneManager.SwitchPanels(SceneDirection.exit);
    }

    public void SetAcriveOfExitButton(bool b)
    {
        exitButton.SetActive(b);
    }

    public void KeyBind()
    {

    }
    public void Apply() 
    {
    
    }
    public void RestoreDefault()
    {

    }
    public void FullScreen()
    {

    }
    public void MouseSensitivity()
    {

    }
    public void MasterVolume()
    {

    }
    public void Language()
    {

    }
}
