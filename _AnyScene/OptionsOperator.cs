using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsOperator : MonoBehaviour
{
    [SerializeField]
    private GameObject exitButton;
    [SerializeField]
    private SceneManager sceneManager;
    [SerializeField]
    private AudioMixerGroup mixerMasterGroup;
    [SerializeField]
    public Slider sensitivitySlider;
    [SerializeField]
    public Slider volumeSlider;
    private static OptionsOperator instance;

    void Awake() 
    {
        if (sceneManager == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("AnyScene");
            sceneManager = go.GetComponent<SceneManager>();
        }
    }

    public void RefreshInstance()
    {
        instance = this;
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
        mixerMasterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80f, 0, GetVolume()));
        SaveManager.SaveOptions();
    }

    public static float GetVolume()
    {
        if(instance == null)
        {
            new Exception("Singleton exeption!");
            return 0;
        }

        //volumeSlider minValue = -1, minValue = 1
        return instance.volumeSlider.value / 2 + .5f;
    }

    public void Language()
    {

    }

    public static OptionsParameters GetParameters()
    {
        if (instance == null)
        {
            new Exception("Singleton exeption!");
            return default;
        }

        return new OptionsParameters(false, instance.volumeSlider.value, 0.5f, 0);
    }

    public static void LoadOptions()
    {
        var parametrs = SaveManager.LoadOptions();
        //fullScreen = parametrs.fullScreen;
        instance.volumeSlider.value = parametrs.volume;
        instance.sensitivitySlider.value = parametrs.sensitivit;
        //Language = parametrs.language;
    }
}

[System.Serializable]
public struct OptionsParameters
{
    public bool fullScreen;
    public float volume;
    public float sensitivit;
    public int language;

    public OptionsParameters(bool fullScreen, float volume, float sensitivit, int language)
    {
        this.fullScreen = fullScreen;
        this.volume = volume;
        this.sensitivit = sensitivit;
        this.language = language;
    }
}
