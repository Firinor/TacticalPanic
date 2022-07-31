using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class OptionsOperator : SinglBehaviour<OptionsOperator>
    {
        [SerializeField]
        private GameObject exitButton;
        [SerializeField]
        private AudioMixerGroup mixerMasterGroup;
        [SerializeField]
        public Slider sensitivitySlider;
        [SerializeField]
        public Slider volumeSlider;
        [SerializeField]
        private AnimationCurve curve;

        private static bool OnLoad;

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
            if (instance == null)
                return;

            float volume = Mathf.Lerp(-80f, 0, curve.Evaluate(GetVolume()));

            mixerMasterGroup.audioMixer.SetFloat("MasterVolume", volume);
            if (!OnLoad)
                SaveManager.SaveOptions();
        }

        public static float GetVolume()
        {
            if (instance == null)
                return default;

            //volumeSlider minValue = -1, minValue = 1
            return instance.volumeSlider.value / 2 + .5f;
        }

        public void Language()
        {

        }

        public static OptionsParameters GetParameters()
        {
            return new OptionsParameters(false, instance.volumeSlider.value, 0.5f, 0);
        }

        public static void LoadOptions()
        {
            if (instance == null)
                return;

            OnLoad = true;
            var parametrs = SaveManager.LoadOptions();
            //fullScreen = parametrs.fullScreen;
            instance.volumeSlider.value = parametrs.volume;
            instance.sensitivitySlider.value = parametrs.sensitivit;
            //Language = parametrs.language;
            OnLoad = false;
        }
    }

    [Serializable]
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
}
