using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Ruinarc
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/SpawnAudioSettingsScriptableObject", order = 1)]
    public class AudioSettings : ScriptableObject, IAudioSettings
    {
        #region Inner Structure

        public AudioMixer audioMixer;
        private float minVolume = -80f;
        private int normalizer = 20;

        public string MasterGroupVolumeName;
        public string MusicGroupVolumeName;
        public string SFXGroupVolumeName;

        public SavablePlayerPrefsValue<bool> SFXIsMuted;
        public SavablePlayerPrefsValue<bool> MusicIsMuted;

        public SavablePlayerPrefsValue<float> masterVolume;
        public SavablePlayerPrefsValue<float> sfxVolume;
        public SavablePlayerPrefsValue<float> musicVolume;

        #endregion

        #region Interface Realization

        //Mute

        public bool MusicMuted
        {
            get { return MusicIsMuted.Value; }
            set
            {
                MusicIsMuted.Value = value;
                SetMixerGroupMuted(MusicGroupVolumeName, value, musicVolume);
            }
        }

        public bool SFXMuted
        {
            get { return SFXIsMuted.Value; }
            set
            {
                SFXIsMuted.Value = value;
                SetMixerGroupMuted(SFXGroupVolumeName, value, sfxVolume);
            }
        }

        //Volume

        public float MasterVolume
        {
            get { return RecalculateVolumeToSlider(masterVolume.Value); }
            set
            {
                SetMixerGroupVolume(masterVolume, value, MasterGroupVolumeName);
            }
        }

        public float MusicVolume
        {
            get { return RecalculateVolumeToSlider(musicVolume.Value); }
            set
            {
                SetMixerGroupVolume(musicVolume, value, MusicGroupVolumeName);
            }
        }

        public float SFXVolume
        {
            get { return RecalculateVolumeToSlider(sfxVolume.Value); }
            set
            {
                SetMixerGroupVolume(sfxVolume, value, MasterGroupVolumeName);
            }
        }

        private float RecalculateVolumeToSlider(float value)
        {
            return Mathf.Pow(10, value / normalizer);
        }

        //Mixer

        public AudioMixer GetAudioMixer()
        {
            return audioMixer;
        }

        #endregion

        private void SetMixerGroupVolume(SavablePlayerPrefsValue<float> mixerGroup, float value, string mixerGroupName)
        {
            float bottomValueAccordingToScale = 0.0001f;
            Mathf.Clamp(value, bottomValueAccordingToScale, 1);
            value = Mathf.Log10(value) * normalizer;

            mixerGroup.Value = value;
            audioMixer.SetFloat(mixerGroupName, value);
        }

        private void SetMixerGroupMuted(string mixerGroupName, bool isMuted, SavablePlayerPrefsValue<float> mixerGroup)
        {
            audioMixer.SetFloat(mixerGroupName, (isMuted ? minVolume : mixerGroup.Value));
        }

        public void InitSettings()
        {
            SFXIsMuted = new SavablePlayerPrefsValue<bool>("SFXisMuted", false);
            MusicIsMuted = new SavablePlayerPrefsValue<bool>("MusicisMuted", false);

            masterVolume = new SavablePlayerPrefsValue<float>(MasterGroupVolumeName, 0f);
            sfxVolume = new SavablePlayerPrefsValue<float>(SFXGroupVolumeName, 0f);
            musicVolume = new SavablePlayerPrefsValue<float>(MusicGroupVolumeName, 0f);

            audioMixer.SetFloat(MasterGroupVolumeName, masterVolume.Value);
            audioMixer.SetFloat(MusicGroupVolumeName, (MusicIsMuted.Value ? minVolume : musicVolume.Value));
            audioMixer.SetFloat(SFXGroupVolumeName, (SFXIsMuted.Value ? minVolume : sfxVolume.Value));
        }
    }
}
