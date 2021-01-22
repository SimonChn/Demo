using UnityEngine.Audio;
using UnityEngine;

namespace Ruinarc
{
    public class BackgroundMusic : AudioObject
    {
        public BackgroundMusic(AudioClip clip, Transform audioCarrier, AudioMixerGroup audioMixerGroup) : base(clip, audioCarrier, audioMixerGroup)
        {
            audioSourse.ignoreListenerPause = true;
        }
    }
}