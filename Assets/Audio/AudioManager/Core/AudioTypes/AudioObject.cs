using UnityEngine;
using UnityEngine.Audio;

namespace Ruinarc
{
    public class AudioObject
    {
        public Transform audioCarrier;
        public AudioClip clip;
        public AudioSource audioSourse;

        public bool canBeDeleted = false;


        public AudioObject(AudioClip clip, Transform audioCarrier, AudioMixerGroup audioMixerGroup)
        {
            this.clip = clip;
            this.audioCarrier = audioCarrier;

            audioSourse = audioCarrier.gameObject.AddComponent<AudioSource>();
            audioSourse.outputAudioMixerGroup = audioMixerGroup;
            audioSourse.clip = this.clip;

        }

        public void Clear()
        {
            audioCarrier = null;
            clip = null;

            if (audioSourse)
            {
                UnityEngine.Object.Destroy(audioSourse);
                audioSourse = null;
            }

            canBeDeleted = true;
        }
        public void DeleteSelf()
        {
            canBeDeleted = true;
        }
    }
}