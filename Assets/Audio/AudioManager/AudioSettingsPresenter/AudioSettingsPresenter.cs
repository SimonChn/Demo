using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ruinarc
{
    public class AudioSettingsPresenter : MonoBehaviour
    {
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider sfxSlider;

        public Toggle muteMusic;
        public Toggle muteSFX;

        private IAudioSettings audSettings;

        private void Start()
        {
            audSettings = AudioManager.Instance.audioSettings;

            SetupSlider(masterSlider, audSettings.MasterVolume, SetMasterVolume);
            SetupSlider(musicSlider, audSettings.MusicVolume, SetMusicVolume);
            SetupSlider(sfxSlider, audSettings.SFXVolume, SetSFXVolume);

            SetupToggle(muteMusic, audSettings.MusicMuted, ChangeMusicMuted);
            SetupToggle(muteSFX, audSettings.SFXMuted, ChangeSFXMuted);
        }

        private void SetupSlider(Slider slider, float initVolume, UnityAction<float> sliderValueChanged)
        {
            if (slider == null)
            {
                return;
            }

            slider.value = initVolume;
            slider.onValueChanged.AddListener(sliderValueChanged);
        }

        private void SetupToggle(Toggle toggle, bool isOn, UnityAction<bool> toggleValueChanged)
        {
            if (toggle == null)
            {
                return;
            }

            toggle.isOn = isOn;
            toggle.onValueChanged.AddListener(toggleValueChanged);
        }

        #region Set

        public void SetMasterVolume(float sliderValue)
        {
            audSettings.MasterVolume = sliderValue;
        }

        public void SetMusicVolume(float sliderValue)
        {
            audSettings.MusicVolume = sliderValue;
        }

        public void SetSFXVolume(float sliderValue)
        {
            audSettings.SFXVolume = sliderValue;
        }

        public void ChangeMusicMuted(bool isOn)
        {
            audSettings.MusicMuted = isOn;
        }

        public void ChangeSFXMuted(bool isOn)
        {
            audSettings.SFXMuted = isOn;
        }

        #endregion
    }
}
