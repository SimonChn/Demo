using UnityEngine;
using UnityEngine.Audio;
using System;

namespace Ruinarc
{
    public static class AudioFabric
    {
        public static AudioObject CreateAudioObject(AudioClip clip, Transform audioCarrier, AudioObjectType audioObjectType = AudioObjectType.SFX2D)
        {
            AudioObject audioObject = null;

            if(audioObjectType == AudioObjectType.SFX2D)
            {
                audioObject = CreateSFX2D(clip, audioCarrier);
            }

            if(audioObjectType == AudioObjectType.Music)
            {
                audioObject = CreateMusic(clip, audioCarrier);
            }

            if(audioObject == null)
            {
                throw new ArgumentException("Magic in AudioObject creation");
            }

            return audioObject;
        }

        private static AudioObject CreateSFX2D(AudioClip clip, Transform audioCarrier)
        {
            SFX2D sfx2D = new SFX2D(clip,audioCarrier, GetMixerGroup("SFX"));           
            return sfx2D;
        }

        private static AudioObject CreateMusic(AudioClip clip, Transform audioCarrier)
        {
            BackgroundMusic music = new BackgroundMusic(clip, audioCarrier, GetMixerGroup("Music"));
            return music;
        }

        private static AudioMixerGroup GetMixerGroup(string groupName)
        {
            IAudioSettings audioSettings = AudioManager.Instance.audioSettings;
            AudioMixerGroup audioMixerGroup = audioSettings.GetAudioMixer().FindMatchingGroups(groupName)[0];

            return audioMixerGroup;
        }
    }
}
